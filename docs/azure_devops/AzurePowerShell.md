# Azure Power Shell Task

## Azure DevOps Input

### AzurePowerShell (v1 - v5) Task using Inline script

```yaml
- task: AzurePowerShell@5
  displayName: 'Azure PowerShell Script: Inline Script Version'
  inputs:
    azureSubscription: 'valet-app-service-transformer-test'   # Required
    ScriptType: InlineScript                                  # Required
    Inline: |
     Write-Host "Testing 1.2..3..."
     Get-ChildItem
    FailOnStandardError: true                                 # Optional
    preferredAzurePowerShellVersion: 3.1.0                    # Required
    pwsh: true                                                # Optional
    workingDirectory: williamh                                # Optional
  continueOnError: true
```

### AzurePowerShell (v1 - v5) Task using FilePath to script

```yaml
- task: AzurePowerShell@5
  displayName: 'Azure PowerShell script: ScriptPath Version'
  inputs:
    azureSubscription: 'valet-app-service-transformer-test'   # Required
    ScriptPath: 'williamh/AzurePowerShell_Task_TestScript.ps1'
    ScriptArguments: '-Title "Test Header"'                   # Optional
    ScriptType: 'FilePath'                                    # Required
    FailOnStandardError: true                                 # Optional
    preferredAzurePowerShellVersion: 3.1.0                    # Required
    pwsh: true                                                # Optional
    workingDirectory: williamh                                # Optional
  continueOnError: true
```

### AzurePowerShell (v2) Task using Classic subscription

```yaml
- task: AzurePowerShell@2
  displayName: 'Azure PowerShell script: ScriptPath Version'
  inputs:
    azureClassicSubscription: 'valet-app-service-transformer-test'    # Required
    azureConnectionType: 'Azure Classic'                              # Required
    ScriptPath: 'williamh/AzurePowerShell_Task_TestScript.ps1'
    ScriptArguments: '-Title "Test Header"'                           # Optional
    azurePowerShellVersion: LatestVersion                             # Required
    ScriptType: 'FilePath'                                            # Required
```

## Transformed Github Action

### Corresponding AzurePowerShell (v1 - v5) action using Inline script

```yaml
- name: 'Azure PowerShell script: Inline Script Version'
  continue-on-error: true
  uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: 'Azure PowerShell script: Inline Script Version'
  continue-on-error: true
  uses: azure/powershell@v1
  with:
    inlineScript: |-
      cd williamh
      Write-Host "Testing 1.2..3..."
      Get-ChildItem
    errorActionPreference: Stop
    failOnStandardError: true
    azPSVersion: 3.1.0
```

### Corresponding AzurePowerShell (v1 - v5) action using path to script

```yaml
- name: 'Azure PowerShell script: ScriptPath Version'
  continue-on-error: true
  uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: 'Azure PowerShell script: ScriptPath Version'
  continue-on-error: true
  uses: azure/powershell@v1
  with:
    inlineScript: |-
      cd williamh
      williamh/AzurePowerShell_Task_TestScript.ps1 -Title "Test Header"
    errorActionPreference: Stop
    failOnStandardError: true
    azPSVersion: 3.1.0
```

### Corresponding AzurePowerShell (v2) action using Classic subscription

```yaml
- name: 'Azure PowerShell script: ScriptPath Version'
  continue-on-error: true
  uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: 'Azure PowerShell script: ScriptPath Version'
  continue-on-error: true
  uses: azure/powershell@v1
  with:
    inlineScript: williamh/AzurePowerShell_Task_TestScript.ps1 -Title "Test Header"
    errorActionPreference: Stop
    failOnStandardError: false
    azPSVersion: latest
```

## Multiple versions (v1 - v5)

- azureSubscription || ConnectedServiceNameARM
- Inline
- ScriptArguments
- ScriptPath
- ScriptType

### V1 - V3

- azureClassicSubscription || ConnectedServiceName
- azureConnectionType || ConnectedServiceNameSelector

### V2 - V5

- azurePowerShellVersion || TargetAzurePS
- azurePowerShellVersion || CustomTargetAzurePs

### V3 - V5

- errorActionPreference
- FailOnStandardError

### V4 and V5

- workingDirectory

## Unsupported Inputs and Aliases

- pwsh
- RestrictContextToCurrentTask
