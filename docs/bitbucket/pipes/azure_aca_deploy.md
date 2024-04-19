# Azure Aca Deploy

[BitBucket Azure Aca Deploy Documentation](https://bitbucket.org/atlassian/azure-aca-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/azure-aca-deploy:0.1.1
  variables:
    AZURE_CLIENT: $AZURE_CLIENT
    AZURE_CLIENT_SECRET: $AZURE_CLIENT_SECRET
    AZURE_TENANT_ID: $AZURE_TENANT_ID
    AZURE_RESOURCE_GROUP: 'my-group'
    AZURE_CONTAINER_APP_NAME: 'my-container'
    CONFIG_PATH: 'config.yaml'
```

## Transformed GitHub Action
```yaml
- uses: azure/login@v1.4.6
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: azure/container-apps-deploy-action@v1
  with:
    yamlConfigPath: config.yaml
    containerAppName: my-container
    resourceGroup: my-group
```

## Unsupported Options
- DEBUG