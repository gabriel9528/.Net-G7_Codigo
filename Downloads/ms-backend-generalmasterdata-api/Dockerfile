#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN apt-get update && apt-get install -y fontforge cabextract wget
RUN wget https://gist.githubusercontent.com/maxwelleite/10774746/raw/ttf-vista-fonts-installer.sh -q -O - | bash
# RUN fc-list

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "ms-backend-generalmasterdata-api.dll"]
#registro de prueba mas data de pruebaaa otraaa
#registro de prueba mas data de pruebaaa otraaassss