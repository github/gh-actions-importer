# Building and Running Locally

This guide explains how to build the GitHub Actions Importer locally, create a Docker image, and run migrations without the `gh` CLI extension.

## Quick Start

### Prerequisites

- Docker (installed and running)
- .NET 6 SDK (for local compilation, optional if using Docker build)
- Ollama (for AI-assisted migrations)

### 1. Clone the Repository

```bash
git clone https://github.com/github/gh-actions-importer.git
cd gh-actions-importer
```

### 2. Build the Docker Image

```bash
# Build locally
docker build -t gh-actions-importer-agent:latest .

# View the image
docker images | grep gh-actions-importer
```

### 3. Run a Command

```bash
# Create your configuration
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  gh-actions-importer-agent:latest \
  configure

# Run an audit
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  -v /path/to/pipelines:/workspace \
  gh-actions-importer-agent:latest \
  audit --source-dir /workspace
```

## Detailed Setup

### Option 1: Using Docker Compose (Recommended for Development)

The included `docker-compose.yml` sets up both Ollama and the migration agent:

```bash
# Start Ollama and pull the model (first time)
docker-compose up ollama
ollama pull gemma4:e4b  # In another terminal

# Start both services
docker-compose up -d

# Run commands
docker-compose exec migration-agent configure --ai-assisted
docker-compose exec migration-agent audit --source-dir /workspace

# View logs
docker-compose logs -f migration-agent
docker-compose logs -f ollama

# Stop services
docker-compose down
```

### Option 2: Manual Docker Setup

#### Step 1: Start Ollama (on Host)

```bash
# macOS/Linux
brew install ollama

# Start Ollama service
ollama serve

# Pull the model (in another terminal)
ollama pull gemma4:e4b
```

#### Step 2: Build the Image

```bash
docker build -t gh-actions-importer-agent:latest .
```

#### Step 3: Create Configuration

```bash
# Create .env.local
cp .env.local.example .env.local

# Edit with your credentials
# GITHUB_ACCESS_TOKEN=your_token
# JENKINS_ACCESS_TOKEN=your_token
# etc.
```

#### Step 4: Run Commands

```bash
# Alias for convenience
alias importer='docker run --rm \
  -v $(pwd)/.env.local:/app/.env.local \
  -v $(pwd):/workspace \
  --network host \
  gh-actions-importer-agent:latest'

# Use the alias
importer configure --ai-assisted
importer audit --source-dir /workspace/my-pipelines
```

### Option 3: Local Compilation (.NET)

If you have .NET 6 SDK installed:

```bash
# Build the .NET project
cd src
dotnet build ActionsImporter.sln -c Release

# Run directly
./ActionsImporter/bin/Release/net6.0/gh-actions-importer configure
```

## Running Migrations

### Example: Jenkins to GitHub Actions

```bash
# 1. Configure (one-time)
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  gh-actions-importer-agent:latest \
  configure --ai-assisted

# 2. Audit Jenkins pipelines
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  -v /path/to/jenkins-pipelines:/workspace \
  gh-actions-importer-agent:latest \
  audit \
    --source-dir /workspace \
    --output-dir /workspace/audit-results

# 3. Dry run (generate workflows without applying)
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  -v /path/to/jenkins-pipelines:/workspace \
  gh-actions-importer-agent:latest \
  dry-run \
    --source-dir /workspace \
    --output-dir /workspace/workflows

# 4. Actual migration
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  -v /path/to/jenkins-pipelines:/workspace \
  gh-actions-importer-agent:latest \
  migrate \
    --source-dir /workspace \
    --output-dir /workspace/workflows
```

## Dockerfile Breakdown

The `Dockerfile` uses a multi-stage build:

**Build Stage:**
- Uses `mcr.microsoft.com/dotnet/sdk:6.0`
- Compiles the .NET project for Linux
- Produces a single, self-contained executable

**Runtime Stage:**
- Uses `mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine` (minimal)
- Copies the compiled binary
- Installs required tools (curl, docker-cli)
- Runs as non-root user (importer:importer)

Result: ~500MB image (lightweight)

## Docker Network Configuration

### macOS/Windows

Docker Desktop provides `host.docker.internal` for accessing the host:

```bash
docker run --rm \
  -e OLLAMA_API_ENDPOINT=http://host.docker.internal:11434 \
  gh-actions-importer-agent:latest \
  configure --ai-assisted
```

### Linux

Linux Docker doesn't support `host.docker.internal`. Use the bridge gateway:

```bash
docker run --rm \
  -e OLLAMA_API_ENDPOINT=http://172.17.0.1:11434 \
  gh-actions-importer-agent:latest \
  configure --ai-assisted
```

Or get the host IP:

```bash
HOST_IP=$(hostname -I | awk '{print $1}')
docker run --rm \
  -e OLLAMA_API_ENDPOINT=http://${HOST_IP}:11434 \
  gh-actions-importer-agent:latest \
  configure --ai-assisted
```

## Environment Variables for Docker

```bash
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  -e OLLAMA_API_ENDPOINT=http://host.docker.internal:11434 \
  -e OLLAMA_MODEL=gemma4:e4b \
  -e YAML_VERBOSITY=debug \
  gh-actions-importer-agent:latest \
  configure --ai-assisted
```

## Volume Mounting

| Volume | Purpose | Example |
|--------|---------|---------|
| `.env.local` | Credentials & config | `-v ~/.env.local:/app/.env.local` |
| Source pipelines | Files to migrate | `-v ./my-pipelines:/workspace` |
| Output | Generated workflows | `-v ./output:/output` |
| Docker socket | For nested Docker calls | `-v /var/run/docker.sock:/var/run/docker.sock` |

## Troubleshooting

### "Cannot connect to Docker daemon"

Ensure Docker is running:
```bash
docker ps
```

### "Ollama not found"

Ensure Ollama is running and accessible:
```bash
curl http://localhost:11434/api/tags
```

If on Linux, use the correct IP:
```bash
docker run --rm \
  -e OLLAMA_API_ENDPOINT=http://172.17.0.1:11434 \
  gh-actions-importer-agent:latest \
  configure --ai-assisted
```

### "Permission denied" on .env.local

Ensure the file is readable:
```bash
chmod 644 .env.local
```

### "Out of disk space"

Building Docker images can require significant space. Clean up:
```bash
docker system prune -a
```

## Performance Tips

1. **Reuse containers**: Use `--network host` when accessing local services
2. **Minimal base image**: The Dockerfile uses Alpine for a small footprint
3. **Parallel migrations**: Run multiple migrations in separate containers
4. **GPU support**: Enable GPU in Ollama for 5-10x faster inference

## Next Steps

- Read [AI_ASSISTED_MIGRATION.md](./AI_ASSISTED_MIGRATION.md) for detailed AI features
- See the main [README.md](./README.md) for migration documentation
- Check vendor-specific guides in `docs/` directory

## Development

### Building from Source

```bash
cd src
dotnet restore ActionsImporter.sln
dotnet build ActionsImporter.sln -c Release
dotnet test ActionsImporter.UnitTests/ActionsImporter.UnitTests.csproj

# Run directly
./ActionsImporter/bin/Release/net6.0/gh-actions-importer configure
```

### Running Tests

```bash
cd src
dotnet test --verbosity detailed
```

### Code Style

```bash
cd src
dotnet format ActionsImporter.sln --verify-no-changes
```

### Contributing

When making changes:
1. Build locally to verify compilation
2. Run tests
3. Test the Docker build
4. Document any new features
