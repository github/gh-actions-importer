# Azure Web Apps Deploy

[BitBucket Azure Web Apps Deploy Documentation](https://bitbucket.org/atlassian/azure-web-apps-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/azure-web-apps-deploy:1.1.0
  variables:
    AZURE_APP_ID: $AZURE_APP_ID
    AZURE_PASSWORD: $AZURE_PASSWORD
    AZURE_TENANT_ID: $AZURE_TENANT_ID
    AZURE_RESOURCE_GROUP: $AZURE_RESOURCE_GROUP
    AZURE_APP_NAME: "my-site"
    ZIP_FILE: "my-package.zip"
    AZURE_CLOUD_ENVIRONMENT: "AzureUSGovernment"
```

## Transformed GitHub Action

```yaml
- uses: azure/login@v1.4.6
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
    environment: AzureUSGovernment
- uses: azure/webapps-deploy@v2.2.5
  with:
    app-name: my-site
    package: my-package.zip
    resource-group-name: "$AZURE_RESOURCE_GROUP"
```

## Unsupported Options

- DEBUG
