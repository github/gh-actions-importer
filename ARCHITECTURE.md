# Architecture Overview

## Project Structure

```
gh-actions-importer/
├── src/
│   ├── ActionsImporter/                 # Main CLI application
│   │   ├── Commands/                    # CLI commands (Configure, Audit, Migrate, etc.)
│   │   ├── Handlers/                    # Command handlers
│   │   ├── Services/                    # Business logic
│   │   │   ├── OllamaService.cs         # NEW: LLM communication
│   │   │   ├── MigrationAssistantService.cs  # NEW: AI-assisted migrations
│   │   │   ├── ConfigurationService.cs  # Configuration management
│   │   │   ├── DockerService.cs         # Docker integration
│   │   │   └── ProcessService.cs        # Process execution
│   │   ├── Interfaces/                  # Service interfaces
│   │   │   ├── IOllamaService.cs        # NEW: Ollama interface
│   │   │   ├── IMigrationAssistantService.cs  # NEW: Assistant interface
│   │   │   └── [other interfaces]
│   │   ├── Models/                      # Data models
│   │   │   ├── OllamaModels.cs          # NEW: Ollama request/response models
│   │   │   ├── MigrationAssistantModels.cs  # NEW: Assistant models
│   │   │   └── [other models]
│   │   ├── App.cs                       # Main application class
│   │   ├── Program.cs                   # Entry point
│   │   └── Constants.cs                 # Configuration constants
│   └── ActionsImporter.UnitTests/       # Unit tests
├── Dockerfile                            # NEW: Build Docker image locally
├── docker-compose.yml                    # NEW: Local dev environment
├── AI_ASSISTED_MIGRATION.md              # NEW: AI feature guide
├── BUILDING_LOCALLY.md                   # NEW: Build & run guide
├── ARCHITECTURE.md                       # This file
└── docs/                                 # Vendor-specific documentation
    ├── jenkins/
    ├── gitlab/
    ├── azure-devops/
    └── [other platforms]
```

## Key Classes

### Application Entry Point

**Program.cs**
- Initializes services (Docker, Configuration, Ollama, MigrationAssistant)
- Sets up CLI command structure
- Parses arguments and invokes commands

**App.cs**
- Core application logic
- Orchestrates services for different commands
- Manages Docker image pulls and updates
- New: Manages AI-assisted configuration flow

### Services

#### OllamaService
```csharp
public class OllamaService : IOllamaService
{
    Task<bool> IsAvailableAsync()           // Health check
    Task<string> GenerateAsync()            // Single response
    IAsyncEnumerable<string> GenerateStreamingAsync()  // Streaming
    Task<List<string>> GetAvailableModelsAsync()  // List models
}
```

Handles HTTP communication with Ollama REST API. Includes:
- Endpoint configuration via environment variables
- Request/response serialization
- Error handling with helpful messages
- Support for streaming responses

#### MigrationAssistantService
```csharp
public class MigrationAssistantService : IMigrationAssistantService
{
    Task<string> GetMigrationPlanAsync()    // Full migration plan
    Task<string> RefineWorkflowAsync()      // Improve YAML workflow
    Task<string> GetMigrationTipsAsync()    // Feature-specific guidance
    Task<bool> IsAvailableAsync()           // Check if LLM available
}
```

Provides high-level AI assistance for migrations:
- Builds prompts with vendor-specific context
- Manages system prompts (roles, constraints)
- Handles LLM inference via OllamaService
- Includes error recovery and fallbacks

#### ConfigurationService (Enhanced)
```csharp
ImmutableDictionary<string, string> GetUserInput()           // Original
Task<ImmutableDictionary<string, string>> GetAiAssistedInputAsync()  // NEW
```

Now supports two configuration flows:
- **Standard**: Interactive prompts for credentials
- **AI-Assisted**: LLM-guided configuration with vendor tips

#### DockerService
```csharp
Task ExecuteCommandAsync()      // Run migration in container
Task UpdateImageAsync()         // Pull latest image
Task VerifyImagePresentAsync()  // Ensure image exists
```

Manages Docker container lifecycle and image management.

### Models

#### OllamaModels.cs (NEW)
```csharp
OllamaGenerateRequest   // Request to Ollama API
OllamaGenerateResponse  // Response from Ollama
OllamaTagsResponse      // Available models list
OllamaModel             // Model metadata
```

#### MigrationAssistantModels.cs (NEW)
```csharp
MigrationAssistantContext   // Context for LLM (pipeline, audit summary, docs)
MigrationAssistantResult    // Result with warnings and metadata
```

### Commands

#### Configure (Enhanced)
```csharp
--features              // Configure feature flags
--ai-assisted           // NEW: AI-guided configuration
```

Other commands:
- **Audit**: Analyze current CI/CD pipelines
- **Forecast**: Predict migration effort
- **DryRun**: Generate workflows without applying
- **Migrate**: Apply actual migration
- **ListFeatures**: Show available features

## Data Flow

### Standard Configuration Flow
```
User Input
    ↓
ConfigurationService.GetUserInput()
    ↓
Prompt each variable (name, password)
    ↓
Store in .env.local
```

### AI-Assisted Configuration Flow (NEW)
```
User selects providers
    ↓
For each provider:
    ├─ MigrationAssistant.GetMigrationTipsAsync()
    │   ↓
    │   OllamaService.GenerateAsync()
    │   ↓
    │   Show tips to user
    │
    └─ Prompt for credentials (with context)
    ↓
Store in .env.local
```

### Migration with AI Refinement (Planned)
```
Original Pipeline (Jenkins YAML)
    ↓
Docker Container:
    ├─ Run audit (importer tool)
    ├─ Generate draft workflow
    └─ Return results
    ↓
MigrationAssistant.RefineWorkflowAsync()
    ↓
OllamaService.GenerateAsync()
    ↓
Improved Workflow (GitHub Actions YAML)
```

## Dependency Injection Pattern

Services are injected through constructors:

```csharp
// In Program.cs
var ollamaService = new OllamaService();
var migrationAssistant = new MigrationAssistantService(ollamaService);
var app = new App(
    dockerService,
    processService,
    configurationService,
    environmentVariables,
    migrationAssistant  // NEW
);
```

Benefits:
- Easy to mock for testing
- Clear dependencies
- Flexible configuration

## Environment Variables

### New for AI Features
| Variable | Default | Purpose |
|----------|---------|---------|
| `OLLAMA_API_ENDPOINT` | `http://localhost:11434` | Where to reach Ollama |
| `OLLAMA_MODEL` | `gemma4:e4b` | Which model to use |

### Existing
| Variable | Purpose |
|----------|---------|
| `GITHUB_ACCESS_TOKEN` | GitHub authentication |
| `CONTAINER_REGISTRY` | Docker registry (default: ghcr.io) |
| Various provider tokens | CI/CD platform authentication |

## Docker Architecture

### Multi-Stage Build

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0
RUN dotnet publish ... → gh-actions-importer binary

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine
COPY binary from stage 1
RUN apk add curl docker-cli
USER importer (non-root)
```

Benefits:
- Small final image (~500MB)
- Minimal dependencies
- Security (non-root user)

### Network Communication

```
Container (CLI)
    ↓ (via host.docker.internal:11434)
Host Ollama Instance
```

Configuration:
- **macOS/Windows**: `host.docker.internal` automatically available
- **Linux**: Need to use bridge IP (`172.17.0.1`) or `--network host`

## Error Handling

### OllamaService
```csharp
// Returns false if Ollama unavailable
Task<bool> IsAvailableAsync()

// Throws InvalidOperationException with helpful message
Task<string> GenerateAsync()
```

### MigrationAssistantService
```csharp
try {
    var plan = await GetMigrationPlanAsync(...);
} catch (InvalidOperationException ex) {
    // Thrown if Ollama not available
    Console.Error.WriteLine($"Error: {ex.Message}");
}
```

### App.ConfigureAsync (Enhanced)
```csharp
if (args.Contains("--ai-assisted")) {
    var ollamaAvailable = await _migrationAssistantService.IsAvailableAsync();
    if (!ollamaAvailable) {
        Console.Error.WriteLineAsync("Error: Ollama not available");
        return 1;
    }
}
```

## Testing Strategy

### Unit Tests Updated

**AppTests.cs**
- Added `Mock<IMigrationAssistantService>`
- Updated `App` constructor calls
- Existing tests remain unchanged

Example:
```csharp
var migrationAssistant = new Mock<IMigrationAssistantService>();
var app = new App(
    dockerService.Object,
    processService.Object,
    configurationService.Object,
    variables,
    migrationAssistant.Object  // NEW
);
```

### Service Testing (To Be Added)

Recommended tests for new services:
```csharp
[TestFixture]
public class OllamaServiceTests
{
    // Test health check
    // Test generate with valid endpoint
    // Test generate with invalid endpoint
    // Test streaming responses
    // Test model availability
}

[TestFixture]
public class MigrationAssistantServiceTests
{
    // Test migration plan generation
    // Test workflow refinement
    // Test feature tips
    // Test unavailability handling
}
```

## Configuration via Environment

### .env.local File
```bash
GITHUB_ACCESS_TOKEN=ghp_...
JENKINS_ACCESS_TOKEN=...
OLLAMA_API_ENDPOINT=http://localhost:11434
OLLAMA_MODEL=gemma4:e4b
```

### Runtime Overrides
```bash
export OLLAMA_API_ENDPOINT=http://custom-host:11434
./gh-actions-importer configure --ai-assisted
```

## Extension Points

### Adding New Commands

```csharp
// In Commands/ directory
public class MyCommand : BaseCommand
{
    protected override string Name => "mycommand";
    protected override string Description => "...";
    protected override Command GenerateCommand(App app) { ... }
}

// In Program.cs
var command = new RootCommand() {
    new MyCommand().Command(app),  // Add here
    ...
};
```

### Adding New Services

```csharp
// 1. Create interface in Interfaces/
public interface IMyService { ... }

// 2. Implement in Services/
public class MyService : IMyService { ... }

// 3. Register in Program.cs
var myService = new MyService();
var app = new App(..., myService);

// 4. Use in App.cs
private readonly IMyService _myService;
public App(..., IMyService myService) {
    _myService = myService;
}
```

## Performance Considerations

### Ollama Latency
- First inference: ~5 seconds (model load)
- Subsequent: ~0.1-2 seconds per response
- Streaming: Shows progress in real-time

### Docker Container Startup
- ~2 seconds on SSD
- Includes .NET runtime initialization
- Can be parallelized across multiple migrations

### Memory Usage
- Base container: ~100MB
- During inference: +500MB-1GB (Ollama model in host memory)
- Multiple containers: Each needs ~500MB

## Security Considerations

1. **Credentials**
   - Stored in `.env.local` (not in git)
   - Injected as environment variables
   - Never logged

2. **Network**
   - Ollama runs locally (no external API calls)
   - All data stays on user's machine
   - Docker uses internal bridge network

3. **Container**
   - Runs as non-root user
   - No privileged mode needed
   - Minimal attack surface (Alpine base)

4. **Code**
   - No external dependencies on AI services
   - Fully offline after model download
   - Open-source LLM (Gemma4)

## Future Enhancements

### Planned
- [ ] Embedded vendor documentation for RAG
- [ ] Workflow validation with LLM
- [ ] Interactive refinement loop
- [ ] Cost estimation for migrations
- [ ] Template-based migration strategies

### Possible
- [ ] Support for additional LLM models
- [ ] Fine-tuning on GitHub Actions patterns
- [ ] Real-time collaboration features
- [ ] Cloud-based LLM option
- [ ] Batch migration optimization

## Related Documentation

- [AI_ASSISTED_MIGRATION.md](./AI_ASSISTED_MIGRATION.md) - User guide for AI features
- [BUILDING_LOCALLY.md](./BUILDING_LOCALLY.md) - Build and deployment guide
- [README.md](./README.md) - Project overview
- [docs/](./docs/) - Vendor-specific migration guides
