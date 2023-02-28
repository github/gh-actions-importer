# Invoke RestApi task

## Azure DevOps input

```yaml
- task: InvokeRESTAPI@1
  inputs:
    connectionType: 'connectedServiceNameARM'
    azureServiceConnection: "123-456-789"
    method: 'GET'
    headers: '{ Content-Type:application/json }' 
    urlSuffix: 'subscriptions/123/resources?api-version=2019-07-01'
    waitForCompletion: 'false'
```

## Transformed Github Action

```yaml
- name: 'Invoke REST API: GET'
  uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: 'Invoke REST API: GET'
  shell: bash
  run: |-
    az rest --method GET \
    --url https://management.azure.com/subscriptions/123/resources?api-version=2019-07-01 \
    --headers Content-Type="application/json"
```

## Unsupported inputs
- Completion Event type Callback (waitForCompletion == true)