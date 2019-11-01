FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj .
RUN dotnet restore
ARG ConnectionString 
ENV ConnectionString =$ConnectionString 
ARG DatabaseName 
ENV DatabaseName =$DatabaseName 
ARG JobCollectionName 
ENV JobCollectionName =$JobCollectionName 
ARG JwtIssuer
ENV JwtIssuer =$JwtIssuer 
ARG JwtKey
ENV JwtKey =$JwtKey 
# copy everything else and build app
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Freelance-Api.dll"]