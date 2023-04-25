# AzureFunction Task

## Azure DevOps Input

```yaml
- task: AzureFunction@1
  inputs:
    function: https://test.azurewebsites.net/api/HttpExample
    key: afasdfasd/Q==
    method: 'GET'
    queryParameters: 'name=matz'
    headers: '{Content-Type:application/json}' 
    successCriteria:  'eq(root['status'], 'successful')'                    
```

### Transformed Github Action

```yaml
- name: https://test.azurewebsites.net/api/HttpExample
  shell: bash
  run: |-
    curl -f -X "GET" https://test.azurewebsites.net/api/HttpExample?code=afasdfasd/Q==&name=matz \
    -H Content-Type:application/json \
    | jq -e '${{env.SUCCESS_CRITERIA }}'
  env:
    SUCCESS_CRITERIA: UPDATE_ME
```

### Unsupported Inputs

- Completion Event type Callback (waitForCompletion == true)
