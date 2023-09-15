# Azure Arm Deploy

[BitBucket Azure Arm Deploy Documentation](https://bitbucket.org/atlassian/azure-arm-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/azure-arm-deploy:1.0.1
  variables:
    AZURE_APP_ID: $AZURE_APP_ID
    AZURE_PASSWORD: $AZURE_PASSWORD
    AZURE_TENANT_ID: $AZURE_TENANT_ID
    AZURE_RESOURCE_GROUP: 'my-resource-group'
    AZURE_LOCATION: 'CentralUS'
    AZURE_DEPLOYMENT_NAME: 'my-deployment'
    AZURE_DEPLOYMENT_MODE: 'Complete'
    AZURE_DEPLOYMENT_TEMPLATE_URI: 'https://myresource/azuredeploy.json'
    AZURE_DEPLOYMENT_PARAMETERS: 'https://mysite/params.json --parameters SomeValue=This SomeOtherValue=That'
    AZURE_DEPLOYMENT_ROLLBACK_ON_ERROR: 'true'
    AZURE_DEPLOYMENT_NO_WAIT: 'true'
    DEBUG: 'true'
```

## Transformed GitHub Action
```yaml
- uses: azure/login@v1.4.6
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: Create Azure Resource Group
  uses: azure/cli@v1.0.7
  with:
    inlineScript: |
      #!/bin/bash
      az group create --name "my-resource-group" --location "CentralUS"
      echo "Azure resource group created"
- name: Deploy Azure ARM Template
  uses: azure/arm-deploy@v1.0.9
  with:
    scope: resourcegroup
    deploymentName: my-deployment
    deploymentMode: Complete
    template: https://myresource/azuredeploy.json
    resourceGroupName: my-resource-group
    failOnStdErr: 'true'
    additionalArguments: "--parameters https://mysite/params.json --parameters SomeValue=This SomeOtherValue=That --no-wait --rollback-on-error"
```

## Unsupported Options
DEBUG