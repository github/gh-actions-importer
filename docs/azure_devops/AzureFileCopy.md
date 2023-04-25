# AzureFileCopy Task

## Azure DevOps Input

```yaml
# Azure file copy
# Copy files to Azure Blob Storage or virtual machines
- task: AzureFileCopy@4
  inputs:
    sourcePath:
    azureSubscription:
    destination: # Options: azureBlob, azureVMs
    storage:
    #containerName: # Required when destination == AzureBlob
    #blobPrefix: # Optional
    #resourceGroup: # Required when destination == AzureVMs
    #resourceFilteringMethod: 'machineNames' # Optional. Options: machineNames, tags
    #machineNames: # Optional
    #vmsAdminUserName: # Required when destination == AzureVMs
    #vmsAdminPassword: # Required when destination == AzureVMs
    #targetPath: # Required when destination == AzureVMs
    #additionalArgumentsForBlobCopy: # Optional
    #additionalArgumentsForVMCopy: # Optional
    #enableCopyPrerequisites: false # Optional
    #copyFilesInParallel: true # Optional
    #cleanTargetBeforeCopy: false # Optional
    #skipCACheck: true # Optional
    #sasTokenTimeOutInMinutes: # Optional
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- run: az storage blob upload-batch --auth-mode login --account-name "" --destination "" --source "" --subscription ""
```

### Unsupported Inputs

- resourceGroup
- resourceFilteringMethod
- machineNames
- vmsAdminUserName
- vmsAdminPassword
- targetPath
- additionalArgumentsForBlobCopy
- additionalArgumentsForVMCopy
- enableCopyPrerequisites
- copyFilesInParallel
- cleanTargetBeforeCopy
- skipCACheck
- sasTokenTimeOutInMinutes

> Only `azureBlob` is supported as a `destination` (not `azureVMs`)
