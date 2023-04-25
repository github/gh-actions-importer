# Team PR Push Trigger

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.tfs.TeamPRPushTrigger plugin="tfs@5.157.1">
    <spec/>
    <jobContext>Jenkins PR build</jobContext>
    <targetBranches>*/master</targetBranches>
</hudson.plugins.tfs.TeamPRPushTrigger>
```

### Transformed Github Action

```yaml
pull_request:
    branches:
        - master
```

### Unsupported Options

- Job Context

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
