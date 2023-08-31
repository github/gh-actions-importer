# PublishBuildArtifacts Task

## Azure DevOps Input

```yaml
# Publish build artifacts to Azure Pipelines or a Windows file share
- task: PublishBuildArtifacts@1
  inputs:
    pathToPublish: artifact.zip
    artifactName: "drop"
    #publishLocation: 'Container' # Options: container, filePath
    #targetPath: # Required when publishLocation == FilePath
    #parallel: false # Optional
    #parallelCount: # Optional
    #fileCopyOptions: #Optional
```

### Transformed Github Action

```yaml
- uses: actions/upload-artifact@v2
  with:
    name: drop
    path: artifact.zip
```

### Unsupported Inputs

- publishLocation
- targetPath
- parallel
- parallelCount
- fileCopyOptions
