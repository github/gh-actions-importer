# Azure Acr Push Image

[BitBucket Azure Acr Push Image Documentation](https://bitbucket.org/atlassian/azure-acr-push-image)

## Bitbucket Input

```yaml
- pipe: atlassian/azure-acr-push-image:0.1.0
  variables:
    AZURE_TOKEN_NAME: $AZURE_TOKEN_NAME
    AZURE_TOKEN_PWD: $AZURE_TOKEN_PWD
    AZURE_REGISTRY: "https://myregistryname.azurecr.io"
    IMAGE_NAME: "my-acr-image"
    TAGS: 'my-tag latest'
```

## Transformed GitHub Action
```yaml
- uses: azure/docker-login@v1.0.1
  with:
    username: "$AZURE_TOKEN_NAME"
    password: "$AZURE_TOKEN_PWD"
    login-server: https://myregistryname.azurecr.io
- name: Push to Azure Container Registry
  run: |-
    docker push https://myregistryname.azurecr.io/my-acr-image:my-tag
    docker push https://myregistryname.azurecr.io/my-acr-image:latest
```

## Unsupported Options
- DEBUG
