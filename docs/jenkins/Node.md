# Node

## Designer pipeline

This plugin is not supported in Designer pipelines.

## Jenkinsfile pipeline

### Jenkins input

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
