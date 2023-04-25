# Sql Dacpac Deployment On Machine Group Task

## Azure DevOps Input

```yaml
- task: SqlDacpacDeploymentOnMachineGroup@0
  inputs:
    TaskType: 'dacpac'
    DacpacFile: 'AwesomeCompany.dacpac'
    TargetMethod: 'server'
    ServerName: 'testing.database.windows.net'
    DatabaseName: 'testing'
    AuthScheme: 'sqlServerAuthentication'
    SqlUsername: 'matz'
    SqlPassword: 'password'
```

## Transformed Github Action

```yaml
- name: Install Powershell Module TaskModuleSqlUtility
  shell: pwsh
  run: Install-Module -Name TaskModuleSqlUtility -Scope CurrentUser -Force
- shell: pwsh
  run: |-
    $sqlPassword = "${{ env.SQL_PASSWORD }}" | ConvertTo-SecureString  -AsPlainText -Force
    $sqlServerCredentials = New-Object System.Management.Automation.PSCredential ("${{ env.SQL_USER }}", $sqlPassword)
    Invoke-DacpacDeployment -dacpacFile "${{ env.DACPAC_FILE }}" -targetMethod "server" -serverName "testing.database.windows.net" -databaseName "testing" -authscheme "sqlServerAuthentication" -sqlServerCredentials $sqlServerCredentials
  env:
    DACPAC_FILE: AwesomeCompany.dacpac
    SQL_USER: matz
    SQL_PASSWORD: "${{ secrets.SQL_DEPLOY_PASSWORD }}"
```

## Unsupported Inputs and Aliases
- Execute within a Transaction
- Acquire an exclusive app lock while executing script(s)
- App Lock Name
