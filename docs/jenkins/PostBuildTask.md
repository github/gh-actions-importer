# Post Build Task

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.postbuildtask.PostbuildTask plugin="postbuild-task@1.9">
  <tasks>
    <hudson.plugins.postbuildtask.TaskProperties>
      <logTexts>
      <hudson.plugins.postbuildtask.LogProperties>
        <logText>IOException</logText>
        <operator>AND</operator>
      </hudson.plugins.postbuildtask.LogProperties>
      <hudson.plugins.postbuildtask.LogProperties>
        <logText>BUILD FAILED</logText>
        <operator>AND</operator>
      </hudson.plugins.postbuildtask.LogProperties>
      </logTexts>
    <EscalateStatus>false</EscalateStatus>
    <RunIfJobSuccessful>true</RunIfJobSuccessful>
    <script>script.sh</script>
    </hudson.plugins.postbuildtask.TaskProperties>
  </tasks>
</hudson.plugins.postbuildtask.PostbuildTask>
```

### Transformed Github Action

```yaml
name: Post Build Task
shell: bash
run: script.sh
if: always()
```

### Unsupported Options

- logText
- EscalateStatus

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
