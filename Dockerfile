FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/api/*.csproj ./
RUN dotnet restore

# copy everything else and build
COPY src/api/ ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/aspnetcore:2.0.4
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "api.dll"]
