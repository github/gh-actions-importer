# Echo

## Designer pipeline

This plugin is not supported in Designer pipelines.

## Jenkinsfile pipeline

### Jenkins input

```groovy
steps {
      echo "this is an echo"
  }
```

### Transformed Github Action

```yaml
jobs:
  job-name:
    - name: echo message
      run: echo "This is a message"
```

### Unsupported Options

- None
