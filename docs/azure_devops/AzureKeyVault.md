# Azure Key Vault

## Azure DevOps Input

```yaml
- task: AzureKeyVault@2
  inputs:
    azureSubscription: 'Your-Azure-Subscription'
    KeyVaultName: 'Your-Key-Vault-Name'
    SecretsFilter: '*'
    RunAsPreJob: false
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: '${{ secrets.AZURE_CREDENTIALS }}'
- name:
  shell: bash
  run: |-
    for secret_name in $(az keyvault secret list --vault-name Your-Key-Vault-Name --query "[].{name:name}" --output tsv); do
      secret_value=$(az keyvault secret show --vault-name "Your-Key-Vault-Name" --name $secret_name --query value -o tsv)
      echo "::add-mask::$secret_value"
      echo "$secret_name=$secret_value" >> $GITHUB_ENV
    done
```

### Unsupported Inputs and Aliases

- azureSubscription
- ConnectedServiceName
