# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY TicketsApi/*.csproj ./TicketsApi/
RUN dotnet restore ./TicketsApi/TicketsApi.csproj

COPY . .
RUN dotnet publish ./TicketsApi/TicketsApi.csproj -c Release -o /out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:$PORT
ENTRYPOINT ["dotnet", "TicketsApi.dll"]
