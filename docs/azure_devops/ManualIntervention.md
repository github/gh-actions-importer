# Manual Intervention task

## Azure DevOps input

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

## Unsupported inputs and aliases
- instructions
- emailRecipients
- onTimeout