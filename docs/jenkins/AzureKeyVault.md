# AzureKeyVault

## Designer Pipeline

This plugin is not supported in Designer Pipelines.

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
options {
    azureKeyVault([[envVariable: 'MY_SECRET', name: 'my-secret', secretType: 'Secret']])
}
steps {
    echo 'Found $MY_SECRET'
}
```

### Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
- name: Get secrets from Azure Key Vault
  shell: bash
  run: |-
    for secret_name in my-secret; do
      secret_value=$(az keyvault secret show --vault-name "chaseTestVault" --name $secret_name --query value -o tsv)
      echo "::add-mask::$secret_value"
      echo "$secret_name=$secret_value" >> $GITHUB_ENV
    done
- name: checkout
  uses: actions/checkout@v2
- name: echo message
  run: echo Found $MY_SECRET
```

### Unsupported Options

- None
