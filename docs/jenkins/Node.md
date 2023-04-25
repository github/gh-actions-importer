# Node

## Designer Pipeline

This plugin is not supported in Designer Pipelines.

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
agent {
    node {
        label 'my-defined-label'
        customWorkspace '/some/other/path'
    }
}
```

### Transformed Github Action

```yaml
run:
  working-directory: "/some/other/path"
```

### Unsupported Options

- label
