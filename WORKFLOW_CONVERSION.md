# AI-Assisted Workflow Conversion

## Overview

The GitHub Actions Importer now supports **AI-assisted workflow conversion** using a local Ollama LLM instance. When you use the `--ai-assisted` flag with `dry-run` or `migrate` commands, the tool leverages the Gemma 4 model to intelligently convert CI/CD workflows from various platforms to GitHub Actions YAML format.

## Key Features

### 1. **LLM-Powered Conversion**
- Uses your local Ollama instance (no external API calls)
- Gemma 4:e4b model for accurate conversions
- Preserves original workflow intent and functionality

### 2. **Smart Conversion Strategy**
- **Mirror Conversion**: Maintains original variable names and structure
- **Direct Equivalents**: Maps direct feature equivalents when available
- **Intelligent Workarounds**: Redefines functionality when no direct equivalent exists
- **Comment Preservation**: Keeps original comments; adds new ones only for workarounds

### 3. **Web Search Fallback**
- When the LLM is uncertain about a conversion, it can search the web (via Ollama's web search)
- Retrieves current best practices and GitHub Actions documentation
- Refines conversion with additional context

### 4. **Supported Platforms**
- Jenkins (Jenkinsfile)
- GitLab CI (.gitlab-ci.yml)
- Azure DevOps (YAML pipelines)
- CircleCI (config.yml)
- Travis CI (.travis.yml)
- Bamboo (YAML specs)
- Bitbucket Pipelines

## Usage

### Basic Usage

```bash
# With dry-run (preview converted workflows)
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  -v ./my-pipelines:/workspace \
  gh-actions-importer-agent:latest \
  dry-run jenkins --source-dir /workspace --ai-assisted

# With migrate (convert and create pull request)
docker run --rm \
  -v ~/.env.local:/app/.env.local \
  -v ./my-pipelines:/workspace \
  gh-actions-importer-agent:latest \
  migrate jenkins \
    --source-dir /workspace \
    --target-url https://github.com/myorg/myrepo \
    --ai-assisted
```

### With Docker Compose

```bash
# Start services
docker-compose up -d

# Run AI-assisted dry-run
docker-compose exec migration-agent dry-run jenkins \
  --source-dir /workspace \
  --ai-assisted
```

## How It Works

### Conversion Pipeline

```
User Input (e.g., Jenkinsfile)
    ↓
1. Load workflow and preserve comments
    ↓
2. Build LLM system prompt with conversion rules
    ↓
3. Call Ollama with source workflow
    ↓
4. LLM generates GitHub Actions YAML
    ↓
5. Validate conversion completeness
    ├─ If complete ✓ → Return result
    ├─ If incomplete → Try web search
    │   ├─ Search for clarification on unclear features
    │   ├─ Refine prompt with search results
    │   └─ Call LLM again with enhanced context
    └─ Return result
    ↓
GitHub Actions Workflow (YAML)
```

### Conversion Rules

The LLM follows these rules when converting workflows:

1. **Preserve Structure**: Keep the logical flow of the original workflow
2. **Mirror Variables**: Use the same variable names and definitions
3. **Direct Mapping**: Use GitHub Actions equivalents for direct feature matches
   - Jenkins stages → GitHub Actions jobs
   - Jenkins environment variables → GitHub Actions env/secrets
   - Parallel jobs → Parallel GitHub Actions jobs

4. **Functional Equivalence**: When no direct equivalent:
   - Redefine functionality to match original intent
   - Use GitHub Actions marketplace actions when appropriate
   - Add minimal clarifying comments

5. **Comment Handling**:
   - Original comments → Preserved in output
   - New comments → Only for workarounds that need clarification
   - No excessive commenting

### Example: Jenkins → GitHub Actions

**Input (Jenkinsfile):**
```groovy
pipeline {
    agent any
    
    environment {
        VERSION = '1.0.0'
        REGISTRY = 'docker.io'
    }
    
    stages {
        stage('Build') {
            steps {
                sh 'npm install'
                sh 'npm run build'
            }
        }
        
        stage('Test') {
            parallel {
                stage('Unit Tests') {
                    steps {
                        sh 'npm run test:unit'
                    }
                }
                stage('Integration Tests') {
                    steps {
                        sh 'npm run test:integration'
                    }
                }
            }
        }
    }
}
```

**Output (GitHub Actions):**
```yaml
name: Build and Test

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

env:
  VERSION: '1.0.0'
  REGISTRY: 'docker.io'

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-node@v3
        with:
          node-version: '16'
      
      - name: Install dependencies
        run: npm install
      
      - name: Build
        run: npm run build
  
  test:
    runs-on: ubuntu-latest
    strategy:
      matrix:
        test-type: [unit, integration]
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-node@v3
        with:
          node-version: '16'
      
      - name: Install dependencies
        run: npm install
      
      - name: Run ${{ matrix.test-type }} tests
        run: npm run test:${{ matrix.test-type }}
```

## Environment Variables

| Variable | Default | Description |
|----------|---------|-------------|
| `OLLAMA_API_ENDPOINT` | `http://localhost:11434` | Ollama API endpoint |
| `OLLAMA_MODEL` | `gemma4:e4b` | Model to use for conversion |

## Prerequisites

### 1. Start Ollama
```bash
ollama serve
```

### 2. Pull the Model
```bash
ollama pull gemma4:e4b
```

### 3. Build Docker Image
```bash
docker build -t gh-actions-importer-agent:latest .
```

### 4. Create .env.local
```bash
cp .env.local.example .env.local
# Edit with your credentials
```

## Conversion Settings

You can customize conversion behavior via environment variables or code:

```csharp
var settings = new ConversionSettings
{
    PreserveComments = true,      // Keep original comments
    MinimalComments = true,        // Only add comments for workarounds
    MirrorConversion = true,       // Preserve variable names/structure
    EnableWebSearch = true,        // Use web search if needed
    MaxWebSearches = 3            // Max web searches per conversion
};
```

## Performance

### Timing
- **First conversion**: ~5-10 seconds (includes model load)
- **Subsequent conversions**: ~2-5 seconds per conversion
- **With web search**: Add 5-15 seconds per search

### Resource Requirements
- **Ollama Host**: 8-16GB RAM, 4+ CPU cores, GPU recommended
- **Docker Container**: 2-4GB RAM, 2+ CPU cores

### Optimization Tips
1. Keep Ollama running between conversions
2. Use GPU if available (5-10x faster)
3. Disable web search for faster conversions (if not needed)
4. Batch multiple conversions in sequence

## Troubleshooting

### "Ollama is not available"
```bash
# Check Ollama is running
curl http://localhost:11434/api/tags

# Start Ollama if needed
ollama serve
```

### "Model not found"
```bash
# Pull the model
ollama pull gemma4:e4b

# Verify it's available
curl http://localhost:11434/api/tags | grep gemma4
```

### Incomplete Conversions
If the LLM produces incomplete YAML:
1. Check the conversion has valid YAML structure (name, on, jobs)
2. Enable web search: `EnableWebSearch = true`
3. Try a different model: `OLLAMA_MODEL=mistral`
4. Review the original workflow for complexity

### Timeout Issues
If conversions time out:
1. Increase Docker timeout: `--timeout 300`
2. Use smaller model: `OLLAMA_MODEL=mistral:7b`
3. Disable web search: `EnableWebSearch = false`
4. Reduce workflow complexity

## Advanced Usage

### Custom Conversion Settings

```csharp
// In your code using the service
var request = new WorkflowConversionRequest
{
    SourceFormat = "Jenkins",
    WorkflowContent = jenkinsFileContent,
    SourceFilename = "Jenkinsfile",
    ProjectContext = "Payment service - microservices architecture"
};

var settings = new ConversionSettings
{
    PreserveComments = true,
    MinimalComments = true,
    MirrorConversion = true,
    EnableWebSearch = true,
    MaxWebSearches = 5
};

var result = await converterService.ConvertAsync(request, settings);
```

### Streaming Conversions

For real-time progress during conversion:

```csharp
await foreach (var chunk in converterService.ConvertStreamingAsync(request))
{
    Console.Write(chunk);  // Stream output as it's generated
}
```

### Web Search Queries

The service automatically generates relevant web search queries when uncertain:
- "How to implement Docker builds in GitHub Actions"
- "GitHub Actions equivalent of Jenkins parallel stages"
- "Running integration tests in GitHub Actions"

## Supported Conversion Features

### Jenkins → GitHub Actions
- ✅ Agents → runners/containers
- ✅ Environment variables → env/secrets
- ✅ Stages → jobs
- ✅ Steps → steps with actions
- ✅ Parallel stages → matrix jobs
- ✅ Triggers → on events
- ✅ Post actions → if conditions + cleanup jobs
- ✅ Parameters → workflow_dispatch inputs
- ✅ Credentials → GitHub secrets

### GitLab CI → GitHub Actions
- ✅ Stages → jobs
- ✅ Scripts → steps with run
- ✅ Variables → env/secrets
- ✅ Services → containers
- ✅ Artifacts → upload-artifact action
- ✅ Cache → cache action
- ✅ Rules/only/except → if conditions

### Azure DevOps → GitHub Actions
- ✅ Stages → jobs
- ✅ Jobs → jobs with matrix
- ✅ Steps → steps
- ✅ Tasks → actions
- ✅ Variables → env
- ✅ Templates → reusable workflows (manual)
- ✅ Pools → runners

## Best Practices

1. **Review Converted Workflows**
   - AI conversions are highly accurate but should be reviewed
   - Check for any warnings in conversion output
   - Test workflows before merging

2. **Start with Simple Workflows**
   - Test on single-stage workflows first
   - Graduate to complex multi-stage pipelines
   - Use dry-run to preview before migrating

3. **Preserve Comments**
   - Enable `PreserveComments = true` to keep context
   - This helps with future workflow maintenance

4. **Use Mirror Conversion**
   - Enable `MirrorConversion = true` to maintain structure
   - Helps team recognition of original patterns

5. **Enable Web Search for Complex Cases**
   - Use for sophisticated build systems
   - Provides LLM with latest documentation
   - Can slow down conversion but improves accuracy

## Limitations

- Complex shell scripting may need manual refinement
- Proprietary CI/CD extensions may not convert automatically
- Web search accuracy depends on search results
- Very large workflows (1000+ lines) may be truncated

## Future Enhancements

- [ ] Support for more CI/CD platforms
- [ ] Template-based batch conversions
- [ ] Interactive refinement loop with user feedback
- [ ] Validation and testing of converted workflows
- [ ] Cost estimation for migrations
- [ ] Integration with GitHub Enterprise

## Getting Help

- **Setup issues**: See BUILDING_LOCALLY.md
- **Technical details**: See ARCHITECTURE.md
- **Web issues**: Check Ollama web search logs
- **Conversions failing**: Enable verbose logging in Ollama

---

**Ready to convert your CI/CD workflows?** Start with a `dry-run` to preview the conversions before migrating!
