# JUnit

## Designer Pipeline

### Jenkins Input

```xml
<hudson.tasks.junit.JUnitResultArchiver plugin="junit@1.37">
  <testResults>test-results/**/*.xml</testResults>
  <keepLongStdio>true</keepLongStdio>
  <healthScaleFactor>1.0</healthScaleFactor>
  <allowEmptyResults>true</allowEmptyResults>
</hudson.tasks.junit.JUnitResultArchiver>
```

### Transformed Github Action

```yaml
- name: Publish test results
  uses: EnricoMi/publish-unit-test-result-action@v2.4.1
  if: always()
  with:
    files: test-results/**/*.xml
```

### Unsupported Options

- healthScaleFactor
- keepLongStdio
- allowEmptyResults
- AggregatedTestResultPublisher

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
    junit 'test-results.xml'
  }
```

### Transformed Github Action

```yaml
- name: Publish test results
  uses: EnricoMi/publish-unit-test-result-action@v2.4.1
  if: always()
  with:
    files: test-results.xml
```

### Unsupported Options

- skipPublishingChecks
