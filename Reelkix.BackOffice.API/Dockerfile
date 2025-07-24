# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Install EF CLI globally and expose it via ENV
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy source code
COPY . .

# Restore and publish
RUN dotnet restore ./Reelkix.BackOffice.API/Reelkix.BackOffice.API.csproj
RUN dotnet publish ./Reelkix.BackOffice.API/Reelkix.BackOffice.API.csproj -c Release -o /app/publish

# Runtime stage — use SDK image to retain EF CLI
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS runtime
WORKDIR /app

# EF CLI path
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy published output
COPY --from=build /app/publish .

# Optional: create upload directory
RUN mkdir -p /app/UploadedFiles

ENTRYPOINT ["dotnet", "Reelkix.BackOffice.API.dll"]
