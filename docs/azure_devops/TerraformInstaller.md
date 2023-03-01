# Setup Terraform task

## Azure DevOps input

```yaml
steps:
- task: ms-devlabs.custom-terraform-tasks.custom-terraform-installer-task.TerraformInstaller@0
  displayName: 'Install Terraform'
  inputs:
    terraformVersion: <version>
```

## Transformed Github Action

```yaml
- name: 'Terraform : azurerm'
  uses: hashicorp/setup-terraform@v2
```

## Unsupported inputs and aliases
