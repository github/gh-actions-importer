# Dir

## Designer pipeline

This plugin is not supported in Designer pipelines.

## Jenkinsfile pipeline

### Jenkins input

```groovy
steps {
  dir('doc/config') {
      sh "ant clear build"
    }
  }
```

### Transformed Github Action

```yaml
jobs:
  job-name:
    - name: sh
      shell: bash
      run: ant clear build
      working-directory: "doc/config"
```

### Unsupported Options

- None
