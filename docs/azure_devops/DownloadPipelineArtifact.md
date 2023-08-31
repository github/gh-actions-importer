# DownloadPipelineArtifact Task

## Azure DevOps Input

```yaml
# Download build and pipeline artifacts
- task: DownloadPipelineArtifact@2
  inputs:
    source: "current" # Options: current, specific
    artifact: "drop" # Optional
    path: "out"
    #project: # Required when source == Specific
    #pipeline: # Required when source == Specific
    #preferTriggeringPipeline: false # Optional
    #runVersion: 'latest' # Required when source == Specific# Options: latest, latestFromBranch, specific
    #runBranch: 'refs/heads/master' # Required when source == Specific && RunVersion == LatestFromBranch
    #runId: # Required when source == Specific && RunVersion == Specific
    #tags: # Optional
    #patterns: '**' # Optional
```

```yaml
steps:
- download: string # Required as first property. Specify current, pipeline resource identifier, or none to disable automatic download. 
  artifact: string # Artifact name.. 
  patterns: string # Pattern to download files from artifact. 
  condition: string # Evaluate this condition expression to determine whether to run this task. 
  continueOnError: boolean # Continue running even on failure?.  (false,n,no,off,on,true,y,yes)
  displayName: string # Human-readable name for the task. 
  target: stepTarget # Environment in which to run this task
  enabled: boolean # Run this task when the job runs?.  (false,n,no,off,on,true,y,yes)
  env:  # Variables to map into the process's environment
    string: string # Name/value pairs.
  name: string # ID of the step.  ([-_A-Za-z0-9]*)
  timeoutInMinutes: string # Time to wait for this task to complete before the server kills it. 
  retryCountOnTaskFailure: string # Number of retries if the task fails. 

```

### Transformed Github Action

```yaml
- uses: actions/download-artifact@v2
  with:
    name: drop
    path: out
```

### Unsupported Inputs

- source (specific)
- project
- preferTriggeringPipeline
- runVersion
- runBranch
- runId
- tags
- patterns
- target
- retryCountOnTaskFailure
