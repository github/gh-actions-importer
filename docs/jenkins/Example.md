# EXAMPLE

## Designer Pipeline

This plugin is not supported in Designer Pipelines.

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
      echo "this is an echo"
  }
```

### Transformed Github Action

```yaml
- name: echo message
  run: echo "This is a message"
```

### Unsupported Options

- None
