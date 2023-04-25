# UseDotnet Task

## Azure DevOps Input

```yaml
- task: UseDotNet@2
  displayName: 'Use .NET Core sdk'
  inputs:
    # packageType: sdk
    # version: 2.2.203
    # installationPath: $(Agent.ToolsDirectory)/dotnet
```

> Also supports `DotNetCoreInstaller@0` and `DotNetCoreInstaller@1`

### Transformed Github Action

```yaml
  - name: Use .NET Core sdk
    uses: actions/setup-dotnet@v3.0.3
    with:
      dotnet-version: 2.2.203
```

### Unsupported Inputs

- packageType
- useGlobalJson
- workingDirectory
- includePreviewVersions
- installationPath
- performMultiLevelLookup
