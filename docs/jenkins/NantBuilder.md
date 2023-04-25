# Nant Builder

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.nant.NantBuilder plugin="nant@1.4.3">
  <targets>build</targets>
  <nantBuildFile>path\to\config.xml.build</nantBuildFile>
  <nantName>(Default)</nantName>
  <properties>property1=value1 property2=value2</properties>
</hudson.plugins.nant.NantBuilder>
```

### Transformed Github Action

```yaml
name: Run NAnt
shell: cmd
run: NAnt -D:property1=value1 -D:property2=value2 -buildfile:path\to\config.xml.build build
```

### Unsupported Options

- None

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
