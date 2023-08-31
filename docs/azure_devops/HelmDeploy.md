# Helm Deploy Task

## Azure DevOps Input

```yaml
- task: HelmDeploy@0
  continueOnError: true 
  inputs:
    connectionType: 'Azure Resource Manager'
    azureSubscription: 'test-service-connection'
    azureResourceGroup: 'myResourceGroup'
    kubernetesCluster: 'myAKSCluster'
    namespace: 'default'
    command: 'ls'
```

## Transformed Github Action

```yaml
- uses: azure/aks-set-context@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
    resource-group: myResourceGroup
    cluster-name: myAKSCluster
- shell: bash
  run: helm ls --namespace default
```

## Unsupported Inputs and Aliases
- Enable TLS.  This is only applicable to Helm 2 installs
- Publish pipeline metadata

## Useful links
- ACR credentials for `helm registry login` command.  See [HERE](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-helm-repos#authenticate-with-the-registry)
