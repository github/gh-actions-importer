# Cucumber Builder

## Designer Pipeline

### Jenkins Input

```xml
<net.masterthought.jenkins.CucumberReportPublisher plugin="cucumber-reports@5.5.0">
<fileIncludePattern>**/*.json</fileIncludePattern>
<fileExcludePattern/>
<jsonReportDirectory/>
<reportTitle/>
<failedStepsNumber>-1</failedStepsNumber>
<skippedStepsNumber>-1</skippedStepsNumber>
<pendingStepsNumber>-1</pendingStepsNumber>
<undefinedStepsNumber>-1</undefinedStepsNumber>
<failedScenariosNumber>-1</failedScenariosNumber>
<failedFeaturesNumber>-1</failedFeaturesNumber>
<failedStepsPercentage>0.0</failedStepsPercentage>
<skippedStepsPercentage>0.0</skippedStepsPercentage>
<pendingStepsPercentage>0.0</pendingStepsPercentage>
<undefinedStepsPercentage>0.0</undefinedStepsPercentage>
<failedScenariosPercentage>0.0</failedScenariosPercentage>
<failedFeaturesPercentage>0.0</failedFeaturesPercentage>
<stopBuildOnFailedReport>false</stopBuildOnFailedReport>
<trendsLimit>0</trendsLimit>
<sortingMethod>ALPHABETICAL</sortingMethod>
<mergeFeaturesById>false</mergeFeaturesById>
<mergeFeaturesWithRetest>false</mergeFeaturesWithRetest>
<hideEmptyHooks>false</hideEmptyHooks>
<skipEmptyJSONFiles>false</skipEmptyJSONFiles>
<expandAllSteps>false</expandAllSteps>
<classificationsFilePattern/>
</net.masterthought.jenkins.CucumberReportPublisher>
```

### Transformed Github Action

```yaml
  - name: Cucumber report
    uses: deblockt/cucumber-report-annotations-action@v1.11
    with:
      access-token: "${{ secrets.GITHUB_TOKEN }}"
      path: "**/cucumber-report.json"
```

### Unsupported Options

- failedStepsNumber
- skippedStepsNumber
- pendingStepsNumber
- undefinedStepsNumber
- skipEmptyJSONFiles
- pendingStepsNumber
- undefinedStepsNumber
- failedScenariosNumber
- failedFeaturesNumber
- failedStepsPercentage
- skippedStepsPercentage
- pendingStepsPercentage
- undefinedStepsPercentage
- failedScenariosPercentage
- failedFeaturesPercentage
- stopBuildOnFailedReport
- sortingMethod
- mergeFeaturesById
- mergeFeaturesWithRetest
- hideEmptyHooks
- skipEmptyJSONFiles
- expandAllSteps

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
pipeline {
  agent any
  stages {
    stage('testing pipeline'){
      steps{
        cucumber buildStatus: 'UNSTABLE',
        reportTitle: 'My report',
        fileIncludePattern: '**/*.json',
        trendsLimit: 10,
        classifications: [
            [
                'key': 'Browser',
                'value': 'Firefox'
            ]
        ]
      }
    }
  }
}
```
### Transformed Github Action

```yaml
  - name: Cucumber
    uses: deblockt/cucumber-report-annotations-action@v1.11
    with:
      access-token: "${{ secrets.GITHUB_TOKEN }}"
      path: "**/cucumber-report.json"
```

### Unsupported Options

- failedStepsNumber
- skippedStepsNumber
- pendingStepsNumber
- undefinedStepsNumber
- skipEmptyJSONFiles
- pendingStepsNumber
- undefinedStepsNumber
- failedScenariosNumber
- failedFeaturesNumber
- failedStepsPercentage
- skippedStepsPercentage
- pendingStepsPercentage
- undefinedStepsPercentage
- failedScenariosPercentage
- failedFeaturesPercentage
- stopBuildOnFailedReport
- sortingMethod
- mergeFeaturesById
- mergeFeaturesWithRetest
- hideEmptyHooks
- skipEmptyJSONFiles
- expandAllSteps
