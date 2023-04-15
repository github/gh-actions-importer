# Node JS Build Wrapper

## Designer pipeline

### Jenkins input

```xml
<buildWrappers>
   <jenkins.plugins.nodejs.NodeJSBuildWrapper>
      <nodeJSInstallationName>node</nodeJSInstallationName>
      <cacheLocationStrategy />
   </jenkins.plugins.nodejs.NodeJSBuildWrapper>
</buildWrappers>
```

### Transformed Github Action

None. A manual task will be surfaced to ensure `NodeJs` is available on the runner.

### Unsupported Options

- None

## Jenkinsfile pipeline

This plugin is not mapped to a GitHub Actions equivalent for a Jenkinsfile pipeline.
