# Azure Web App Container Task

## Azure DevOps Input

```yaml
- task: AzureWebApp@1
  inputs:
    azureSubscription: 'ValetServiceConnection'     # Required
    appName: 'container-web-app'                    # Required
    deployToSlotOrASE: true                         # Optional
    slotName: 'staging'                             # Required if deploySlotOrASE == true
    imageName: 'myregistry.azurecr.io/nginx:latest' # Required, Alias: containers
    multicontainerConfigFile: 'docker-compose.yml'  # Optional
    containerCommand: 'dotnet exec filename.dll'    # Optional
    appSettings: '-Port 3001'                       # Optional
    configurationStrings: '-rubyVersion 1.6'        # Optional
    resourceGroupName: 'valet-resource-group'       # Required if deploySlotOrASE == true
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: azure/webapps-deploy@v2
  with:
    app-name: container-web-app
    image-name: myregistry.azurecr.io/nginx:latest
    configuration-file: docker-compose.yml
    container-command: dotnet exec filename.dll
    slot-name: staging
- uses: azure/appservice-settings@v1
  with:
    app-name: container-web-app
    slot-name: staging
    app-settings-json: '[{"name":"PORT","value":"3001","slotSetting":true}]'
    general-settings-json: '{"rubyVersion":"1.6"}'
```

### Unsupported Inputs and Aliases

- azureSubscription
- resourceGroupName
