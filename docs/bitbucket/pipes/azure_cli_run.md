# Azure Cli Run

[BitBucket Azure Cli Run Documentation](https://bitbucket.org/atlassian/azure-cli-run)

## Bitbucket Input

```yaml
pipe: atlassian/azure-cli-run
variables:
  AZURE_APP_ID: $AZURE_APP_ID
  AZURE_PASSWORD: $AZURE_PASSWORD
  AZURE_TENANT_ID: $AZURE_TENANT_ID
  CLI_COMMAND: 'az account show'
```

## Transformed GitHub Action
```yaml
- uses: azure/login@v1.4.6
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: azure/cli@v1.0.7
  with:
    inlineScript: az account show
```

## Unsupported Options
- DEBUG