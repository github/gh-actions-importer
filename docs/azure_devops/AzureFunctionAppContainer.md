# Azure Function App Container Task

## Azure DevOps Input

```yaml
- task: AzureFunctionApp@1
  inputs:
    azureSubscription: ValetSubscription                # Required
    appName: 'valet-container-app'                      # Required
    deployToSlotOrASE: true,                            # Optional
    imageName: 'valetRegistry.azurecr.io/nginx:latest'  # Required
    resourceGroupName: 'unused-resource-group'          # Required if deploySlotOrASE == true
    slotName: 'production-slot'                         # Required if deploySlotOrASE == true
    appSettings:  '-Port 500'                           # Optional
    configurationStrings: '-phpVersion "5.6"'           # Optional
    containerCommand: npm start                         # Optional
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: azure/functions-container-action@v1.2.0
  with:
    app-name: valet-container-app
    image: valetRegistry.azurecr.io/nginx:latest
    container-commad: npm start
- uses: azure/appservice-settings@v1
  with:
    app-name: valet-container-app
    slot-name: production-slot
    app-settings-json: '[{"name":"Port","value":"5000","slotSetting":true}]'
    general-settings-json: '{"phpVersion":"5.6"}'
```

### Unsupported Inputs

- azureSubscription
- resourceGroupName
