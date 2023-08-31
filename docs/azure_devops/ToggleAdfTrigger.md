# Toggle Adf Trigger Task

## Azure DevOps Input

```yaml
- task: toggle-adf-trigger@2
  inputs:
    azureSubscription: 'Subs'
    ResourceGroupName: 'RG'
    DatafactoryName: 'ADF'
    TriggerFilter: '*'
    TriggerStatus: 'Start' 
```

## Transformed Github Action

```yaml
- uses: Azure/login@v1
   with:
     creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: Toggle ADF Trigger
   id: toggle-adf-trigger
   env:
      RESOURCE_GROUP_NAME: RG
      DATA_FACTORY_NAME: ADF
    shell: pwsh
    run: |-
        Set-PSRepository PSGallery -InstallationPolicy Trusted
        Install-Module -Name Az.DataFactory -AllowClobber
        $triggers = Get-AzDataFactoryV2Trigger -ResourceGroupName $env:ResourceGroupName -DataFactoryName $env:DataFactoryName
        foreach($trigger in $triggers){
          Write-Host "Starting Trigger" $trigger.Name
          Start-AzDataFactoryV2Trigger -ResourceGroupName $env:ResourceGroupName -DataFactoryName $env:DataFactoryName -Name $trigger.Name -Force
        }
```

## Unsupported Inputs and Aliases
- azureSubscription
- TriggerFilter
