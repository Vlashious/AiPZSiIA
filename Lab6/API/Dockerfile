FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /api
COPY "published/" .
EXPOSE 5000
ENTRYPOINT ["dotnet", "API.dll"]