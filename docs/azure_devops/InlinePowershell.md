# Inline Powershell Task

## Azure DevOps Input

```yaml
- task: InlinePowershell@1
  inputs:
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
- shell: pwsh
  run: |-
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
