# Shell Builder

## Designer pipeline

### Jenkins input

```xml
<hudson.tasks.Shell>
    <command>echo "hello world"</command>
    <configuredLocalRules/>
</hudson.tasks.Shell>
```

### Transformed Github Action

```yaml
name: run command
shell: bash
run: echo "hello world"
```

### Unsupported Options

The following options are not supported:

- Exit code to set build unstable
- Environment filters

## Jenkinsfile pipeline

### Jenkins input

```groovy
steps {
    sh 'echo "hello world"'
}
```

### Transformed Github Action

```yaml
name: run command
shell: bash
run: echo "hello world"
```

### Unsupported Options

- None