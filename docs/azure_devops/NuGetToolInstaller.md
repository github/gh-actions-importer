# NuGetToolInstaller Task

## Azure DevOps Input

```yaml
# NuGet tool installer
# Acquires a specific version of NuGet from the internet or the tools cache and adds it to the PATH. Use this task to change the version of NuGet used in the NuGet tasks.
- task: NuGetToolInstaller@1
  inputs:
    #versionSpec: # Optional
    #checkLatest: false # Optional
```

> Also supports NuGetToolInstaller@0

### Transformed Github Action

```yaml
- uses: nuget/setup-nuget@v1.1.1
  with:
    nuget-version: '4.3.0'
```

### Unsupported Inputs

- checkLatest
