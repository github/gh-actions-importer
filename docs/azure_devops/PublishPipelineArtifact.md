# PublishPipelineArtifact Task

## Azure DevOps Input

```yaml
# Publish (upload) a file or directory as a named artifact for the current run
- task: PublishPipelineArtifact@1
  inputs:
    targetPath: "artifact.zip" # Required
    artifactName: "drop" # Optional
    #artifactType: 'pipeline' # Required. Options: pipeline, filepath. Default value: pipeline
    #fileSharePath: '\server\folderName' # Required when artifactType = filepath
    #parallel: false # Optional. Default value: false
    #parallelCount: 1 # Optional. Value must be at least 1 and not greater than 128. D

- publish: artifact.zip
  artifact: drop
```

```yaml
steps:
- publish: string # path to a file or folder
  artifact: string # artifact name
  displayName: string  # friendly name to display in the UI
```

### Transformed Github Action

```yaml
- uses: actions/upload-artifact@v2
  with:
    name: drop
    path: artifact.zip
```

### Unsupported Inputs

- artifactType
- fileSharePath
- parallel
- parallelCount
