# VSTest Task

## Azure DevOps Input

```yaml
- task: VSTest@2
  inputs:
    testAssemblyVer2: '**\\*test*.dll'
    searchFolder: 'path/to/tests'
    testFiltercriteria: 'test_filter'
    vstestLocationMethod: 'version'
    vsTestVersion: 'latest'
    runSettingsFile: 'Local.RunSettings'
    pathtoCustomTestAdapters: '/TestAdapterPath'
    runInParallel: true
    runTestsInIsolation: false
    codeCoverageEnabled: true
    otherConsoleOptions: '/Logger:trx'
    platform: 'x64'
```

## Transformed Github Action

```yaml
  uses: microsoft/vstest-action@v1.0.0
  with:
    testAssembly: "**\\\\*test*.dll"
    searchFolder: path/to/tests
    testFiltercriteria: test_filter
    vstestLocationMethod: version
    vsTestVersion: latest
    runSettingsFile: Local.RunSettings
    pathToCustomTestAdapters: "/TestAdapterPath"
    runInParallel: true
    runTestsInIsolation: false
    codeCoverageEnabled: true
    otherConsoleOptions: "/Logger:trx"
    platform: x64
```

### Unsupported Inputs and Aliases

-testSelector:  Unsupported Options: testPlan, testRun
-testPlan: # Required when testSelector == TestPlan
-testSuite: # Required when testSelector == TestPlan
-testConfiguration: # Required when testSelector == TestPlan
-distributionBatchType: 'basedOnTestCases' # Optional. Options: basedOnTestCases, basedOnExecutionTime, basedOnAssembly
-batchingBasedOnAgentsOption: 'autoBatchSize' # Optional. Options: autoBatchSize, customBatchSize
-customBatchSizeValue: '10' # Required when distributionBatchType == BasedOnTestCases && BatchingBasedOnAgentsOption == CustomBatchSize
-batchingBasedOnExecutionTimeOption: 'autoBatchSize' # Optional. Options: autoBatchSize, customTimeBatchSize
-customRunTimePerBatchValue: '60' # Required when distributionBatchType == BasedOnExecutionTime && BatchingBasedOnExecutionTimeOption == CustomTimeBatchSize
-dontDistribute: False # Optional
-failOnMinTestsNotRun: false # Optional
-minimumExpectedTests: '1' # Optional
-diagnosticsEnabled: false # Optional
-collectDumpOn: 'onAbortOnly' # Optional. Options: onAbortOnly, always, never
-rerunFailedTests: False # Optional
-rerunType: 'basedOnTestFailurePercentage' # Optional. Options: basedOnTestFailurePercentage, basedOnTestFailureCount
-rerunFailedThreshold: '30' # Optional
-rerunFailedTestCasesMaxLimit: '5' # Optional
-rerunMaxAttempts: '3' # Optional
