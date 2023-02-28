# Timeout

## Designer pipleline

This plugin is not supported in Designer pipelines.

## Jenkinsfile pipeline

### Jenkins input

```groovy
options {
    timeout(time: 1, unit: 'HOURS')
}
```

### Transformed Github Action

```yaml
job:
  timeout-minutes: 60
```

### Jenkins input

```groovy
steps {
  timeout(time: 3, unit: 'MINUTES') {
    echo 'Hello World!'
  }
}
```

### Transformed Github Action

```yaml
- name: echo message
  run: echo Hello World!
  timeout-minutes: 3
```

### Unsupported Options

`timeout` is not supported when set at the pipeline level of a Jenkinsfile
