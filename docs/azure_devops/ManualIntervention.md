# Manual Intervention Task

## Azure DevOps Input

```yaml
- task: ManualIntervention@8
  inputs:
```

## Transformed Github Action

```yaml
jobs:
  build:
    environment:
      name: approval_required
```

## Unsupported Inputs and Aliases
- instructions
- emailRecipients
- onTimeout
