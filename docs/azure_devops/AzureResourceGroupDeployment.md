# AzureResourceGroupDeployment@2 Task

Also supports the [AzureResourceManagerTemplateDeployment@3 task](https://github.com/microsoft/azure-pipelines-tasks/tree/master/Tasks/AzureResourceManagerTemplateDeploymentV3).

## Azure DevOps Input

```yaml
# Azure resource group deployment
# Deploy an Azure Resource Manager (ARM) template to a resource group and manage virtual machines
- task: AzureResourceGroupDeployment@2
  inputs:
    azureSubscription:
    #action: 'Create Or Update Resource Group' # Options: create Or Update Resource Group, select Resource Group, start, stop, stopWithDeallocate, restart, delete, deleteRG
    resourceGroupName:
    #location: # Required when action == Create Or Update Resource Group
    #templateLocation: 'Linked artifact' # Options: linked Artifact, uRL Of The File
    #csmFileLink: # Required when templateLocation == URL Of The File
    #csmParametersFileLink: # Optional
    #csmFile: # Required when  TemplateLocation == Linked Artifact
    #csmParametersFile: # Optional
    #overrideParameters: # Optional
    #deploymentMode: 'Incremental' # Options: Incremental, Complete, Validate
    #enableDeploymentPrerequisites: 'None' # Optional. Options: none, configureVMwithWinRM, configureVMWithDGAgent
    #teamServicesConnection: # Required when enableDeploymentPrerequisites == ConfigureVMWithDGAgent
    #teamProject: # Required when enableDeploymentPrerequisites == ConfigureVMWithDGAgent
    #deploymentGroupName: # Required when enableDeploymentPrerequisites == ConfigureVMWithDGAgent
    #copyAzureVMTags: true # Optional
    #runAgentServiceAsUser: # Optional
    #userName: # Required when enableDeploymentPrerequisites == ConfigureVMWithDGAgent && RunAgentServiceAsUser == True
    #password: # Optional
    #outputVariable: # Optional
    #deploymentName: # Optional
    #deploymentOutputs: # Optional
    #addSpnToEnvironment: false # Optional
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- run: |-
    az group create --location "westus" --name "foo" --subscription "bar"
    az deployment group create --resource-group "foo" --no-prompt true --subscription "bar" --template-file "template.json"
```

### Unsupported Inputs

- action (when not `create Or Update Resource Group` or `deleteRG`)
- enableDeploymentPrerequisites
- teamServicesConnection
- teamProject
- deploymentGroupName
- copyAzureVMTags
- runAgentServiceAsUser
- userName
- password
- outputVariable
- deploymentOutputs
- addSpnToEnvironment
