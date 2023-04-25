# Sql Dacpac Deploy Task

## Azure DevOps Input

```yaml
- task: SqlDacpacDeploy@1
  inputs:
    machinesList: "db_server.fabrikam.com"
    AdminUserName: "admin"
    AdminPassword: "password123"
    WinRMProtocol: Https
    DacpacFile: update.dacpac
    ServerName: "fabrikam1"
    DatabaseName: fabrikam
```

## Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: Azure/sql-action@v1
  with:
    connection-string: "${{ secrets.AZURE_SQL_CONNECTION_STRING }}"
    path: update.dacpac
    action: publish
```

## Unsupported Inputs and Aliases
- PublishProfile
- DeployInParallel

The following inputs are ignored because it is assumed the target server is a Azure SQL Server and the connection and authentication will be handled by the Azure SQL Deploy [Action](https://github.com/marketplace/actions/azure-sql-deploy)
- AdminUserName
- AdminPassword
- WinRMProtocol
- TestCertificate
- TargetMethod
- ServerName
- DatabaseName
- AuthScheme
- SqlUsername
- SqlPassword
