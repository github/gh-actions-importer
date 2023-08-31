# Databricks Deploy Scripts Task

## Azure DevOps Input

```yaml
- task: databricksDeployScripts@0
  inputs:
    region: 'westus2'                     #Required, Defaults to westeurope
    localPath: "Databricks/Notebooks"     #Required, Defaults to ""
    databricksPath: "/Shared/Notebooks"   #Required, Defaults to "/Shared
    #clean: true                          #Defaults to false
```

## Transformed Github Action

```yaml
- name: Install Databricks CLI
  uses: microsoft/install-databricks-cli@v1.0.0
- name: Databricks Notebooks deployment
  uses: microsoft/databricks-import-notebook@v1.0.0
  with:
    databricks-host: https://westus2.azuredatabricks.net
    databricks-token: "${{ secrets.DATABRICKS_TOKEN }}"
    local-path: Databricks/Notebooks
    remote-path: "/Shared/Notebooks"
```

## Unsupported Inputs and Aliases
- authMethod
- bearerToken
- applicationId
- spSecret
- resourceGroup
- workspace
- subscriptionId
- tenantId
