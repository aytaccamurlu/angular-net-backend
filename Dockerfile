# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Proje dosyalarını kopyala
COPY *.csproj ./
RUN dotnet restore

# Tüm dosyaları kopyala ve publish al
COPY . ./
RUN dotnet publish -c Release -o out

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Build çıktısını al
COPY --from=build /app/out .

# Render PORT env gönderir
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "backend.dll"]
