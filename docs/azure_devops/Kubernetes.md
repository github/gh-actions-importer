# Kubernetes Task

## Azure DevOps Input

```yaml
- task: Kubernetes@1
  inputs:
    connectionType: 'Azure Resource Manager'
    azureSubscriptionEndpoint: 'test-service-connection'
    azureResourceGroup: 'myResourceGroup'
    kubernetesCluster: 'myAKSCluster'
    command: 'get'
    arguments: 'nodes'
    secretType: 'generic'
    secretArguments: '--from-literal=password=1234'
    secretName: 'password'
```

## Transformed Github Action

```yaml
- uses: azure/aks-set-context@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
    resource-group: myResourceGroup
    cluster-name: myAKSCluster
- uses: azure/k8s-create-secret@v1
  with:
    secret-type: generic
    arguments: "--from-literal=password=1234"
    secret-name: password
- shell: bash
  working-directory: "${{ github.workspace }}"
  run: kubectl get nodes -o json
```

## Unsupported Inputs and Aliases
- useClusterAdmin
- Force update secret
- Force update configmap
- Path to kubectl
- Check for latest version
