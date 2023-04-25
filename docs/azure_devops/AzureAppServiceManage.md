# Azure App Service Manage Task

## Azure DevOps Input

```yaml
# Azure App Service manage
# Start, stop, restart, slot swap, slot delete, install site extensions or enable continuous monitoring for an Azure App Service
- task: AzureAppServiceManage@0
  inputs:
    azureSubscription:                # Required, Alias: ConnectedServiceName
    action: 'Swap Slots'              # Optional. Default: Swap Slots
                                      # Options: Swap Slots, Start Swap With Preview, Complete Swap, Cancel Swap, Start Azure App Service, Stop Azure App Service, Restart Azure App Service, Delete Slot, Install Extensions, Enable Continuous Monitoring, Start all continuous webjobs, Stop all continuous webjobs
    webAppName: "App Name"            # Required
    specifySlotOrASE: false           # Optional, Alias: SpecifySlot
    resourceGroupName:                # Required when action == Swap Slots || Action == Delete Slot || SpecifySlot == True
    sourceSlot:                       # Required when action == Swap Slots
    swapWithProduction: true          # Optional, Default: true
    targetSlot:                       # Required when action == Swap Slots && SwapWithProduction == False
    preserveVnet: false               # Optional, Default: false
    slot: 'production'                # Required when action == Delete Slot || SpecifySlot == True, Default: production
    # Unsupported
    extensionsList:                   # Required when action == Install Extensions
    outputVariable:                   # Optional
    appInsightsResourceGroupName:     # Required when action == Enable Continuous Monitoring
    applicationInsightsResourceName:  # Required when action == Enable Continuous Monitoring
    applicationInsightsWebTestName:   # Optional
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- uses: azure/cli@v1.0.7
  with:
  # Action - Start & Stop
    inlineScript: az webapp start --name "web-app" --resource-group "app-resource-group" --subscription "Azure Subscription"
  # Action - Delete Slot
    inlineScript: az webapp deployment slot delete --name "web-app" --resource-group "app-resource-group" --subscription "Azure Subscription" --slot "staging"
  # Action - Swap Slots, add action field for Start With Preview, Complete Swap, Cancel Swap
    inlineScript: az webapp deployment slot swap --name "web-app" --resource-group "app-resource-group" --subscription "Azure Subscription" --slot "staging" --target-slot "targetSlot" --preserve-vnet false --action {preview, swap, reset}
  # Start/Stop all continuous WebJobs
    inlineScript: |-
      WEB_JOBS="$(az webapp webjob continuous list --name "service-app" --resource-group "service-resource-group" --subscription "Azure Subscription" --query "[].name" --output tsv)"
      echo "$WEB_JOBS" | while read line ; do
        web_job_name="${line##*/}"
        az webapp webjob continuous start --name "service-app" --resource-group "service-resource-group" --subscription "Azure Subscription" --slot "slot-staging" --webjob-name "$web_job_name"
      done
```

### Unsupported Inputs

- Enable Continuous Monitoring(Action)
- Install Extensions(Action)
- ExtensionsList(Install Extensions)
- OutputVariable(Install Extensions)
- AppInsightsResourceGroupName(Enable Continuous Monitoring)
- ApplicationInsightsResourceName(Enable Continuous Monitoring)
- ApplicationInsightsWebTestName(Enable Continuous Monitoring)
