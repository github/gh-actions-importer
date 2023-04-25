# Input

## Designer Pipeline

This plugin is not supported in Designer Pipelines.

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
   input message: 'What should we do?', ok: 'Ok', parameters: [choice(choices: ['Deploy', 'Build', 'Test'], description: 'Pick what to do', name: 'TO_DO')]
}
```

### Transformed Github Action

```yaml
jobs:
  ci:
    environment:
      name: approval_required
    runs-on: ubuntu-latest
```

### Unsupported Options

- parameters
- message
- id
- ok
