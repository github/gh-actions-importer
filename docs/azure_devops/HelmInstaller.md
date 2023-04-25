# Helm Installer Task

## Azure DevOps Input

```yaml
- task: HelmInstaller@1
  inputs:
    helmVersionToInstall: '3.7.0'
- task: HelmInstaller@0
  inputs:
    helmVersion: '2.14.1'
    checkLatestHelmVersion: false
    installKubectl: true
    kubectlVersion: '1.9.0'
    checkLatestKubectl: false
```

## Transformed Github Action

```yaml
- uses: azure/setup-helm@v3.5
      with:
        version: v3.7.0
- uses: azure/setup-helm@v3.5
  with:
    version: v2.14.1
- uses: azure/setup-kubectl@v1
  with:
    version: v1.9.0
```

## Unsupported Inputs and Aliases
none
