# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder

WORKDIR /src

# Copy project files
COPY src/ActionsImporter/ ./ActionsImporter/
COPY src/Directory.Build.props .
COPY src/global.json .

# Restore and build the project
RUN dotnet restore ActionsImporter/ActionsImporter.csproj
RUN dotnet publish ActionsImporter/ActionsImporter.csproj \
    -c Release \
    -r linux-x64 \
    --self-contained \
    -o /app \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    -p:TreatWarningsAsErrors=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine

WORKDIR /app

# Copy the built binary from the builder stage
COPY --from=builder /app/gh-actions-importer .

# Install curl for health checks and Docker CLI for nested operations
RUN apk add --no-cache curl docker-cli

# Create a non-root user
RUN addgroup -g 1001 importer && \
    adduser -u 1001 -G importer -s /sbin/nologin importer

# Change ownership of the app directory
RUN chown -R importer:importer /app

USER importer

ENTRYPOINT ["./gh-actions-importer"]
CMD ["--help"]
