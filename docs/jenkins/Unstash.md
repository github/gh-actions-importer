# Unstash

## Designer pipeline

This plugin is not supported in Designer pipelines.

## Jenkinsfile pipeline

### Jenkins input

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
