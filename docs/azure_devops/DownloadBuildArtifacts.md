# DownloadBuildArtifacts Task

## Azure DevOps Input

```yaml
# Download files that were saved as artifacts of a completed build
- task: DownloadBuildArtifacts@0
  inputs:
    #buildType: 'current' # Options: current, specific
    #project: # Required when buildType == Specific
    #pipeline: # Required when buildType == Specific
    #specificBuildWithTriggering: false # Optional
    #buildVersionToDownload: 'latest' # Required when buildType == Specific. Options: latest, latestFromBranch, specific
    #allowPartiallySucceededBuilds: false # Optional
    #branchName: 'refs/heads/master' # Required when buildType == Specific && BuildVersionToDownload == LatestFromBranch
    #buildId: # Required when buildType == Specific && BuildVersionToDownload == Specific
    #tags: # Optional
    #downloadType: 'single' # Choose whether to download a single artifact or all artifacts of a specific build. Options: single, specific
    #artifactName: # Required when downloadType == Single
    #itemPattern: '**' # Optional
    #downloadPath: '$(System.ArtifactsDirectory)'
    #parallelizationLimit: '8' # Optional
```

```yaml
steps:
- downloadBuild: string # Required as first property. ID for the build resource. 
  artifact: string # Artifact name.
  path: string # Path to download the artifact into. 
  patterns: string # Downloads the files which matches the patterns. 
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
- pipeline
- specificBuildWithTriggering
- buildVersionToDownload
- allowPartiallySucceededBuilds
- branchName
- buildId
- tags
- itemPattern
- parallelizationLimit
- target
- retryCountOnTaskFailure
