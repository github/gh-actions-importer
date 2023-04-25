# Python

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.python.Python plugin="python@1.3">
  <command># Script to join two strings\nstring1 = "GitHub " string2 = "Valet"\njoined_string = string1 + string2 print(joined_string)</command>
  <configuredLocalRules/>
</hudson.plugins.python.Python>
```

### Transformed Github Action

```yaml
name: run command
shell: python
run: |-
  # Script to join two strings
  string1 = "GitHub "
  string2 = "Valet"
  joined_string = string1 + string2
  print(joined_string)
```

### Unsupported Options

- None

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
