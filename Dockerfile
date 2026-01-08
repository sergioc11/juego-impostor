# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY ["Antigravity.csproj", "."]
RUN dotnet restore "./Antigravity.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Antigravity.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Antigravity.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Antigravity.dll"]
