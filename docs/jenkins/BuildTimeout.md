# Build Timeout

## Jenkins Input

```xml
<buildWrappers>
  <hudson.plugins.build__timeout.BuildTimeoutWrapper plugin="build-timeout@1.20">
    <strategy class="hudson.plugins.build_timeout.impl.AbsoluteTimeOutStrategy">
      <timeoutMinutes>30</timeoutMinutes>
    </strategy>
    <timeoutEnvVar>timeoutvar</timeoutEnvVar>
    <operationList>
      <hudson.plugins.build__timeout.operations.AbortOperation/>
      <hudson.plugins.build__timeout.operations.FailOperation/>
      <hudson.plugins.build__timeout.operations.WriteDescriptionOperation>
        <description>test</description>
      </hudson.plugins.build__timeout.operations.WriteDescriptionOperation>
    </operationList>
  </hudson.plugins.build__timeout.BuildTimeoutWrapper>
</buildWrappers>
```

## Transformed Github Action

```yaml
jobs:
  build:
   timeout-minutes: 30
```

## Unsupported Options

- Strategies:
  - Deadline
  - Elastic
  - Likely Stuck
  - No Activity
- Time-out variable
- Time-out actions
