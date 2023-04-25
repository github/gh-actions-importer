# Azure Web App Task

## Azure DevOps Input

```yaml
- task: AzureWebApp@1
  inputs:
    azureSubscription: 'ValetServiceConnection'
    appName: 'valet-web-app'
    deployToSlotOrASE: true                      # Optional
    slotName: 'production'                       # Required if deploySlotOrASE == true
    package: 'package-path/'                     # Required, Default: '$(System.DefaultWorkingDirectory)/**/*.zip'
    appSettings: '-Port 5000'                    # Optional
    configurationStrings: '-phpVersion 5.6'      # Optional
    startUpCommand: 'dotnet exec filename.dll'   # Optional, appType == 'functionAppLinux'
    appType: 'webAppLinux'
    resourceGroupName: 'valet-resource-group'    # Required if deploySlotOrASE == true
    runtimeStack: 'NODE|10.1'                    # Optional, appType == 'functionAppLinux'
    deploymentMethod: 'runFromPackage'           # Required, appType == 'functionApp', Default: Auto
    customWebConfig: '-NodeStartFile server.js'  # Optional, appType == 'functionApp'
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: azure/webapps-deploy@v2
  with:
    app-name: valet-web-app
    slot-name: production
    package: package-path/
    startup-command: dotnet exec filename.dll
- uses: azure/appservice-settings@v1
  with:
    app-name: valet-app
    app-settings-json: '[{"name":"PORT","value":"5000","slotSetting":true}]'
    general-settings-json: '{"phpVersion":"5.6"}'
```

### Unsupported Inputs and Aliases

- appType
- azureSubscription
- deploymentMethod
- runtimeStack
- resourceGroupName
- customWebConfig
