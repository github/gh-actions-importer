# Azure Rm Web App Deployment Task

## Azure DevOps Input

```yaml
- task: AzureRmWebAppDeployment@4
  inputs:
    ConnectionType: 'AzureRM'
    azureSubscription: 'ValetServiceConnection'
    appType: 'webApp'
    WebAppName: 'win-app-service-name'
    deployToSlotOrASE: true
    ResourceGroupName: 'valet-rg'
    SlotName: 'staging'
    VirtualApplication: 'virtual-app-field'
    packageForLinux: 'package/path'
    ScriptType: 'Inline Script'
    InlineScript: ':: You can provide your deployment commands here. One command per line.'
    WebConfigParameters: 'web.config'
    AppSettings: '-Port 9000'
    ConfigurationSettings: '-javascriptVersion 3.1'
    enableCustomDeployment: true
    DeploymentType: 'webDeploy'
    SetParametersFile: 'SetParamfile-field'
    RemoveAdditionalFilesFlag: true
    enableXmlTransform: true
    enableXmlVariableSubstitution: true
    JSONFiles: |
      {
        "Data": {
          "DefaultConnection": {
            "ConnectionString": "Server=(localdb)\SQLEXPRESS;Database=MyDB;Trusted_Connection=True"
          }
        }
      }
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: Azure/webapps-deploy@v2
  with:
    app-name: win-app-service-name
    package: package/path
    slot-name: staging
- uses: azure/appservice-settings@v1
  with:
    app-name: win-app-service-name
    slot-name: staging
    app-settings-json: '[{"name":"Port","value":"9000","slotSetting":true}]'
    general-settings-json: '{"javascriptVersion":"3.1"}'

```

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
# The Azure Function App does not accept glob patterns. Consider updating the package path.
- uses: Azure/functions-action@v1
  with:
    app-name: func-app-linux-app-service
    package: "$(System.DefaultWorkingDirectory)/**/*.zip"
    slot-name: prod
- uses: azure/appservice-settings@v1
  with:
    app-name: win-app-service-name
    slot-name: staging
    app-settings-json: '[{"name":"Port","value":"9000","slotSetting":true}]'
    general-settings-json: '{"javascriptVersion":"3.1"}'

```

### Unsupported Inputs and Aliases

#### Multiple versions (2,3, and 4)

- azureSubscription
- resourceGroupName
- apiApp(appType)
- mobileApp(appType)
- deploymentMethod
- RuntimeStack
- VirtualApplication
- RuntimeStackFunction
- ScriptType
- InlineScript
- ScriptPath
- UseWebDeploy || enableCustomDeployment
- SetParametersFile
- RemoveAdditionalFilesFlag
- ExcludeFilesFromAppDataFlag
- AdditionalArguments
- RenameFilesFlag
- enableXmlTransform || XmlTransformation
- enableXmlVariableSubstitution || XmlVariableSubstitution
- JSONFiles
- WebAppUri
- WebConfigParameters (customWebConfig)
- TakeAppOfflineFlag

#### V4

- PublishProfilePath
- PublishProfilePassword
- DeploymentType

#### V3

- ImageSource
- AzureContainerRegistry
- AzureContainerRegistryLoginServer
- AzureContainerRegistryTag
- DockerRepositoryAccess
- RegistryConnectedServiceName
- PrivateRegistryImage
- PrivateRegistryTag
- GenerateWebConfig
