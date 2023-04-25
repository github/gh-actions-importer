# Inline Azure Powershell Task

## Azure DevOps Input

```yaml
- task: InlineAzurePowershell@1
  inputs:
    ConnectedServiceNameSelector: 'ConnectedServiceNameARM'
    ConnectedServiceNameARM: 'service-connection'
    Script: |
      Param(
        [string]$buildNumber,
        [string]$buildStatus
      )
      
      Write-Output "The buildNumber -> $buildNumber"
      Write-Output "build status: -> $buildStatus"
    ScriptArguments: '-buildNumber 29 -buildStatus passing'
```

## Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
    enable-AzPSSession: true
- uses: azure/powershell@v1
  with:
    azPSVersion: latest
    inlineScript: |-
      $params = "-buildNumber 29 -buildStatus passing"
      $scriptBlock = @'
      Param(
        [string]$buildNumber,
        [string]$buildStatus
      )
      Write-Output "The buildNumber -> $buildNumber"
      Write-Output "build status: -> $buildStatus"
      '@
      $func = New-Item -Path function: -Name inline_script_$(Get-Random) -Value $scriptBlock
      Invoke-Expression "$func $params"
```

## Unsupported Inputs and Aliases
- none
