# I I S Web App Deployment On Machine Group task

## Azure DevOps input

```yaml
- task: IISWebAppDeploymentOnMachineGroup@0
  displayName: "Sample Deploy"
  inputs:
    WebSiteName: 'Sample-Website'
    Package: '$(System.DefaultWorkingDirectory)\**\*.zip'
    RemoveAdditionalFilesFlag: true
    TakeAppOfflineFlag: true
    XmlTransformation: true
```

## Transformed Github Action

```yaml
- name: Sample Deploy
  uses: cschleiden/webdeploy-action@v1.1.0
  with:
    webSiteName: Sample-Website
    package: "${{ github.workspace }}\\**\\*.zip"
    removeAdditionalFilesFlag: true
    takeAppOfflineFlag: true
```

## Unsupported inputs and aliases

- VirtualApplication
- XmlTransformation
- AdditionalArguments
- XmlVariableSubstitution
- JSONFiles
- ExcludeFilesFromAppDataFlag
- SetParametersFile
