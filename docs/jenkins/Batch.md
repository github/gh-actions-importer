# Batch Builder

## Designer Pipeline

### Jenkins Input

```xml
<builders>
  <hudson.tasks.BatchFile>
    <command>echo "hello world!"</command>
      <configuredLocalRules>
        <jenkins.tasks.filters.impl.RetainVariablesLocalRule>
          <variables>test</variables>
          <retainCharacteristicEnvVars>true</retainCharacteristicEnvVars>
          <processVariablesHandling>RESET</processVariablesHandling>
        </jenkins.tasks.filters.impl.RetainVariablesLocalRule>
      </configuredLocalRules>
      <unstableReturn>3</unstableReturn>
  </hudson.tasks.BatchFile>
</builders>
```

### Transformed Github Action

```yaml
name: run batch command
run: echo "hello world!"
shell: cmd
```

### Unsupported Options

- Environment filters (configuredLocalRules)
- ERRORLEVEL to set build unstable (unstableReturn)

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
  bat 'set'
}
```

### Transformed Github Action

```yaml
steps:
- name: bat
  shell: cmd
  run: set
```

### Unsupported Options

- None
