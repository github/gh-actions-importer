# Powershell Task

## Azure DevOps Input

```yaml
# Run a PowerShell script on Linux, macOS, or Windows
- task: PowerShell@2
  inputs:
    filePath: ./build.ps1 # Required when targetType == FilePath
    arguments: '-Name someName -Path -Value "Some long string value"' # Optional
    #targetType: 'filePath' # Optional. Options: filePath, inline
    #script: '# Write your PowerShell commands here.Write-Host Hello World' # Required when targetType == Inline
    #errorActionPreference: 'stop' # Optional. Options: stop, continue, silentlyContinue
    #failOnStderr: false # Optional
    #ignoreLASTEXITCODE: false # Optional
    #pwsh: false # Optional
    #workingDirectory: # Optional
```

```yaml
- powershell:  # inline script
  workingDirectory:  #
  displayName:  #
  failOnStderr:  #
  errorActionPreference:  #
  warningPreference:  #
  informationPreference:  #
  verbosePreference:  #
  debugPreference:  #
  ignoreLASTEXITCODE:  #
  env:  # mapping of environment variables to add
```

```yaml
- pwsh:  # inline script
  workingDirectory:  #
  displayName:  #
  failOnStderr:  #
  errorActionPreference:  #
  warningPreference:  #
  informationPreference:  #
  verbosePreference:  #
  debugPreference:  #
  ignoreLASTEXITCODE:  #
  env:  # mapping of environment variables to add
```

### Transformed Github Action

```yaml
- run: ./build.ps1 -Name someName -Path -Value \"Some long string value\"
  shell: powershell
```

### Unsupported Inputs

- failOnStderr
- warningPreference
- informationPreference
- verbosePreference
- debugPreference
