# Unstash

## Designer Pipeline

This plugin is not supported in Designer Pipelines.

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
  unstash name:'MyStashedFiles'
}
```

### Transformed Github Action

```yaml
name: unstash
uses: actions/download-artifact@v2
with:
  name: MyStashedFiles
```

### Unsupported Options

- None
