# NUnit

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.nunit.NUnitPublisher plugin="nunit@0.27">
  <testResultsPattern>TestResults.xml</testResultsPattern>
  <debug>false</debug>
  <keepJUnitReports>false</keepJUnitReports>
  <skipJUnitArchiver>false<skipJUnitArchiver>
  <healthScaleFactor>1.0</healthScaleFactor>
  <failIfNoResults>false</failIfNoResults>
  <failedTestsFailBuild>false</failedTestsFailBuild>
</hudson.plugins.nunit.NUnitPublisher>
```

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
  steps {
    nunit testResultsPattern: 'TestResult.xml'
 }
```
### Transformed Github Action

```yaml
- name: Publish NUnit test results
  uses: EnricoMi/publish-unit-test-result-action@v2.4.1
  if: always()
  with:
    files: TestResult.xml
```
### Unsupported Options

- failIfNoResults
- healthScaleFactor
- debug
- keepJUnitReports
- skipJUnitArchiver
