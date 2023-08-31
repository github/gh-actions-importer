# Deploy Adf Json Task

## Azure DevOps Input

```yaml
task: deploy-adf-json@2
  inputs:
    azureSubscription: 'ff'
    ResourceGroupName: 'rg'
    DatafactoryName: 'adf'
    ServicePath: 'sp'
    DataflowPath: 'df'
    DatasetPath: 'ds'
    PipelinePath: 'pd'
    TriggerPath: 'td'
    Continue: true
    Sorting: 'ascending'
```

## Transformed Github Action

```yaml
- uses: Azure/login@v1
      with:
        creds: "${{ secrets.AZURE_CREDENTIALS }}"
    - name: Deploy ADF JSON
      env:
        RESOURCE_GROUP_NAME: rg
        DATA_FACTORY_NAME: adf
      shell: pwsh
      run: |-
        Set-PSRepository PSGallery -InstallationPolicy Trusted
        Install-Module -Name Az.DataFactory -AllowClobber
        $DataFactory = Get-AzDataFactoryV2 -ResourceGroupName $env:RESOURCE_GROUP_NAME -Name $env:DATA_FACTORY_NAME
        $linked_service_name_file = Get-Content sp -Raw | ConvertFrom-Json
        $linked_service_name = $linked_service_name_file.Name
        Set-AzDataFactoryV2LinkedService -ResourceGroupName $env:RESOURCE_GROUP_NAME -DataFactoryName $env:DATA_FACTORY_NAME -Name $linked_service_name -File sp
        $data_set_name_file = Get-Content ds -Raw | ConvertFrom-Json
        $data_set_name = $data_set_name_file.Name
        Set-AzDataFactoryV2Dataset -ResourceGroupName $env:RESOURCE_GROUP_NAME -DataFactoryName $env:DATA_FACTORY_NAME -Name $data_set_name -DefinitionFile ds
        $data_flow_name_file = Get-Content df -Raw | ConvertFrom-Json
        $data_flow_name = $data_flow_name_file.Name
        Set-AzDataFactoryV2DataFlow -ResourceGroupName $env:RESOURCE_GROUP_NAME -DataFactoryName $env:DATA_FACTORY_NAME -Name $data_flow_name -DefinitionFile df
        $pipeline_name_file = Get-Content pd -Raw | ConvertFrom-Json
        $pipeline_name = $pipeline_name_file.Name
        Set-AzDataFactoryV2Pipeline -ResourceGroupName $env:RESOURCE_GROUP_NAME -DataFactoryName $env:DATA_FACTORY_NAME -Name $pipeline_name -File pd
        $linked_service_name_file = Get-Content td -Raw | ConvertFrom-Json
        $linked_service_name = $linked_service_name_file.Name
        Set-AzDataFactoryV2Trigger -ResourceGroupName $env:RESOURCE_GROUP_NAME -DataFactoryName $env:DATA_FACTORY_NAME -Name $linked_service_name -DefinitionFile td
```

## Unsupported Inputs and Aliases
- azureSubscription
- Continue
- Sorting
