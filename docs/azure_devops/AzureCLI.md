# AzureCLI Task

## Azure DevOps Input

```yaml
- task: AzureCLI@2
  inputs:
    azureSubscription: <Name of the Azure Resource Manager service connection>
    scriptType: ps
    scriptLocation: inlineScript
    inlineScript: |
      az --version
      az account show
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- run: |-
    $ErrorActionPreference = 'stop'
    az --version
    az account show
    if ((Test-Path -LiteralPath variable:\\LASTEXITCODE)) { exit $LASTEXITCODE }
  shell: powershell
```

### Unsupported Inputs

- azureSubscription
- addSpnToEnvironment
- useGlobalConfig
- failOnStandardError
