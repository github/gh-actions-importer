# Azure Web Apps Containers Deploy

[BitBucket Azure Web Apps Containers Deploy Documentation](https://bitbucket.org/atlassian/azure-web-apps-containers-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/azure-web-apps-containers-deploy:1.2.0
  variables:
    AZURE_APP_ID: $AZURE_APP_ID
    AZURE_PASSWORD: $AZURE_PASSWORD
    AZURE_TENANT_ID: $AZURE_TENANT_ID
    AZURE_RESOURCE_GROUP: $AZURE_RESOURCE_GROUP
    AZURE_APP_NAME: $AZURE_APP_NAME
    DOCKER_CUSTOM_IMAGE_NAME: $DOCKER_CUSTOM_IMAGE_NAME
    DOCKER_REGISTRY_SERVER_URL: $DOCKER_REGISTRY_SERVER_URL
    DOCKER_REGISTRY_SERVER_USER: $DOCKER_REGISTRY_SERVER_USER
    DOCKER_REGISTRY_SERVER_PASSWORD: $DOCKER_REGISTRY_SERVER_PASSWORD
    SLOT: 'staging'
    DEBUG: 'true'
```

## Transformed GitHub Action
```yaml
- uses: azure/login@v1.4.6
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: Deploy to Azure Web Apps Containers
  run: |
    az webapp config container set \
    --resource-group $AZURE_RESOURCE_GROUP \
    --name $AZURE_APP_NAME \
    --docker-custom-image-name $DOCKER_CUSTOM_IMAGE_NAME \
    --docker-registry-server-url $DOCKER_REGISTRY_SERVER_URL \
    --docker-registry-server-user $DOCKER_REGISTRY_SERVER_USER \
    --docker-registry-server-password $DOCKER_REGISTRY_SERVER_PASSWORD \
    --slot staging \
    --debug
    WEBAPP_URL=$(az webapp deployment list-publishing-profiles -n $AZURE_APP_NAME n-g $AZURE_RESOURCE_GROUP --query '[0].destinationAppUrl' -o tsv)
    echo "Webapp URL: $WEBAPP_URL"
```

## Unsupported Options
N/A