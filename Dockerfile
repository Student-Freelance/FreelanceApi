FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
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
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
RUN env
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Freelance-Api.dll", "--environment=X"] 