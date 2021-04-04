FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build
COPY . .
RUN dotnet publish -o published
WORKDIR /api
COPY --from=build published/ .
EXPOSE 5000
ENTRYPOINT ["dotnet", "API.dll"]