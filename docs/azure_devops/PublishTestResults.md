# Publish Test Results Task

## Azure DevOps Input

```yaml
- task: PublishTestResults@2
  inputs:
    testResultsFormat: 'JUnit'    # XUnit, NUnit, VSTest, CTest
    testResultsFiles: '/TEST-*.xml'
    searchFolder: 'folder/path'
    mergeTestResults: true
    failTaskOnFailedTests: true
    testRunTitle: 'Junit title'
    buildPlatform: 'build-platform'
    buildConfiguration: 'BuildConfiguration-field'
```

### Transformed Github Action

```yaml
# JUnit
- name: Publish XUnit test results
  uses: EnricoMi/publish-unit-test-result-action@v2.4.1
  if: always()
  with:
    comment_title: XUnit Title
    files: "**/TEST-*.xml"
```

```yaml
# XUnit, NUnit
- name: Publish XUnit test results
  uses: dorny/test-reporter@v1.6.0
  if: success() || failure()
  with:
    name: XUnit Title
    path: /TEST-*.xml
    reporter: dotnet-trx
  working-directory: folder/path
```

### Unsupported Inputs

- testResultsFormat (VSTest, CTest)
- mergeTestResults
- buildPlatform
- buildConfiguration
