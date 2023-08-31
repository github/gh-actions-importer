# Azure Function App Task

## Azure DevOps Input

```yaml
- task: AzureFunctionApp@1
  inputs:
    azureSubscription:                            # Required
    appType: 'functionApp'                        # Required
    appName: 'valet-app'                          # Required
    deployToSlotOrASE: true,                      # Optional
    resourceGroupName: 'unused-resource-group'    # Required if deploySlotOrASE == true
    slotName: 'production-slot'                   # Required if deploySlotOrASE == true
    package: 'package-path/'                      # Required, Default: '$(System.DefaultWorkingDirectory)/**/*.zip'
    runtimeStack: 'NODE|15'                       # Optional, appType == 'functionAppLinux'
    appSettings:  '-PORT 500'                     # Optional
    configurationStrings: '-phpVersion "5.6"'     # Optional
    customWebConfig: '-NodeStartFile server.js'   # Optional, appType == 'functionApp'
    startUpCommand: 'npm start'                   # Optional, appType == 'functionAppLinux'
    deploymentMethod: 'runFromPackage'            # Required, appType == 'functionApp', Default: Auto
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: Azure/functions-action@v1
  with:
    app-name: valet-app
    package: package-path/
    slot-name: production-slot
- uses: azure/appservice-settings@v1
  with:
    app-name: valet-app
    app-settings-json: '[{"name":"PORT","value":"5000","slotSetting":true}]'
    general-settings-json: '{"phpVersion":"5.6"}'
```

### Unsupported Inputs

- appType
- azureSubscription
- deploymentMethod
- resourceGroupName
- runtimeStack
- customWebConfig
