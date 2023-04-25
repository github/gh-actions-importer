# Terraform Task

## Azure DevOps Input

```yaml
- task: TerraformTaskV3@3
  inputs:
    command: plan
    workingDirectory: ''                             # Optional
    commandOptions: '-out=path'                      # Optional
    environmentServiceNameAzureRM: 'uuid'            # Optional
  continueOnError: true                              # Optional

```

## Transformed Github Action

```yaml

- name: Terraform plan
  continue-on-error: true
  run: terraform plan -out=path
  working-directory: "${{ github.workspace }}"
  envs:
    ARM_SUBSCRIPTION_ID: "{{ secrets.ARM_SUBSCRIPTION_ID }}"
    ARM_TENANT_ID: "{{ secrets.ARM_TENANT_ID }}"
    ARM_CLIENT_ID: "{{ secrets.ARM_CLIENT_ID }}"
    ARM_CLIENT_SECRET: "{{ secrets.ARM_CLIENT_SECRET }}"
```

## Unsupported Inputs and Aliases

