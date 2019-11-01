FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app
ARG ConnectionString 
ARG DatabaseName 
ARG JobCollectionName 
ARG JwtIssuer
ARG JwtKey
ENV ConnectionString=$ConnectionString \
DatabaseName=$DatabaseName \
JobCollectionName=$JobCollectionName \
JwtIssuer=$JwtIssuer \
JwtKey=$JwtKey 
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj .
RUN dotnet restore
# copy everything else and build app
WORKDIR /app
COPY . .
RUN dotnet build -c Release -o /app
FROM build AS publish
RUN dotnet publish -c Release -o /app
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/out .
ENTRYPOINT ["dotnet", "Freelance-Api.dll"]