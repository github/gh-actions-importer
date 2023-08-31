# Tokenizer Task

## Azure DevOps Input

```yaml
- task: Tokenizer@2
  inputs:
    SourcePath: '.\settings.xml'
    ConfigurationJsonFile: '$(Build.SourcesDirectory)\token_config.json'
    ReplaceUndefinedValuesWithEmpty: true
```

## Transformed Github Action

```yaml
- shell: pwsh
  run: |-
    $tempFile = "${{ runner.temp }}\$(Split-Path $Env:DESTINATION_PATH -leaf)"
    Copy-Item -Force $Env:SOURCE_PATH $tempFile -Verbose
    Write-Host "Starting token updates using xpaths for environments $validEnviroments"
    $validEnviroments = $Env:ENVIRONMENTS.Split(',') | ForEach {$_.Trim()}
    $config = Get-Content $Env:CONFIG_PATH | ConvertFrom-Json
    $sourceIsXML = [bool]((Get-Content $Env:SOURCE_PATH) -as [xml])
    if($sourceIsXML){
      $xml = [xml](Get-Content $tempFile)
      ForEach ($env in $validEnviroments) {
          $keys = $config.$env.ConfigChanges
          ForEach ($key in $keys) {
              if ($key.NamespaceUrl -And $key.NamespacePrefix) {
                  $ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
                  $ns.AddNamespace($key.NamespacePrefix, $key.NamespaceUrl)
                  $node = $xml.SelectSingleNode($key.KeyName, $ns)
              } else {
                  $node = $xml.SelectSingleNode($key.KeyName)
              }
              if ($node) {
                  Write-Host "Updating $($key.Attribute) of $($key.KeyName): $($key.Value)"
                  $node.($key.Attribute) = $key.Value
              }
          }
      }
      $xml.save($tempFile)
    }
    Write-Host "Starting token updates using pattern '__<pattern>__'"
    $regex = '__[A-Za-z0-9._-]*__'
    $matches = select-string -Path $tempFile -Pattern $regex -AllMatches | % { $_.Matches } | % { $_.Value }
    ForEach ($match in $matches){
        $tokenValue = if($Env:REPLACE_UNDEFINED_WITH_EMPTY -eq "true") {$null} else {$match}
        $token= $match.Trim('_')
        if (Test-Path Env:$token) {
            $tokenValue = (get-item env:$token).Value
        } elseif ($config) {
            $customVar = $validEnviroments | foreach {$config.$_.CustomVariables.$token} | where { $_ } | select -Last 1
            if ($customVar){ $tokenValue = $customVar }
        }
        Write-Host "Found token: $token and updating with value '$tokenValue'"
        (Get-Content $tempFile) |
        Foreach-Object {
            $_ -replace $match, $tokenValue
        } | Set-Content $tempFile -Force
    }
    Copy-Item -Force $tempFile $Env:DESTINATION_PATH
  env:
    SOURCE_PATH: ".\\settings.xml"
    DESTINATION_PATH: ".\\settings.xml"
    ENVIRONMENTS: default
    CONFIG_PATH: "${{ github.workspace }}\\token_config.json"
    REPLACE_UNDEFINED_WITH_EMPTY: true
```

## Unsupported Inputs and Aliases
- None
