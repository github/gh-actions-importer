# Sql Azure Dacpac Deployment Task

## Azure DevOps Input

```yaml
- task: SqlAzureDacpacDeployment@1
  inputs:
    azureConnectionType: 'ConnectedServiceName'
    azureClassicSubscription: 'azure-temp'
    AuthenticationType: 'connectionString'
    ConnectionString: '$(connection_string)'
    deployType: 'DacpacTask'
    DeploymentAction: 'Publish'
    DacpacFile: 'AwesomeCompany.dacpac'
    IpDetectionMethod: 'AutoDetect'
```

## Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
# if 'Allow Azure Services and resources to access this server' is ON the Azure/login step above can likely be removed
- name: Azure SQL Deploy
  uses: Azure/sql-action@v2
  with:
    connection-string: "${{ secrets.AZURE_SQL_CONNECTION_STRING }}"
    path: AwesomeCompany.dacpac
    action: Publish
```

## Unsupported Inputs and Aliases
- deploymentAction: Extract, Export, Import
- publishProfile
- authenticationType: server, aadAuthenticationPassword, aadAuthenticationIntegrated, and servicePrincipal

## Additional Information
- Connection to Azure is handled by the Azure/login action and can be removed if not required to access Server
- Only supported authentication type is connectionString

