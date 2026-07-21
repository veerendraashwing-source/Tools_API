# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

# Copy csproj/sln files first so restore is cached unless dependencies change
COPY EasyChit.sln ./
COPY EasyChit/Easychit_Api.csproj EasyChit/
COPY Easychit_Infrastructure/Easychit_Infrastructure.csproj Easychit_Infrastructure/
COPY Easychit_Repository/Easychit_Repository.csproj Easychit_Repository/
RUN dotnet restore EasyChit.sln

# Copy the rest of the source and publish
COPY EasyChit/ EasyChit/
COPY Easychit_Infrastructure/ Easychit_Infrastructure/
COPY Easychit_Repository/ Easychit_Repository/
RUN dotnet publish EasyChit/Easychit_Api.csproj -c Release -o /app/publish --no-restore

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# appsettings.json is not baked into the image (gitignored, holds secrets) -
# mount it at runtime, e.g.:
#   docker run -v /path/to/appsettings.json:/app/appsettings.json:ro ...
RUN mkdir -p Log_Files Uploads

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Easychit_Api.dll"]
