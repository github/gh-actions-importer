# Setup Terraform Task

## Azure DevOps Input

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

## Unsupported Inputs and Aliases
