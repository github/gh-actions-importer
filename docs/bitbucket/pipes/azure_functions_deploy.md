# Azure Functions Deploy

[BitBucket Azure Functions Deploy Documentation](https://bitbucket.org/atlassian/azure-functions-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/azure-functions-deploy:2.0.0
  variables:
    AZURE_APP_ID: $AZURE_APP_ID
    AZURE_PASSWORD: $AZURE_PASSWORD
    AZURE_TENANT_ID: $AZURE_TENANT_ID
    FUNCTION_APP_NAME: 'my-function'
    ZIP_FILE: 'application.zip'
```

## Transformed GitHub Action
```yaml
- uses: azure/login@v1.4.6
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: azure/functions-action@v1.5.0
  with:
    app-name: my-function
    package: application.zip
```

## Unsupported Options
- DEBUG