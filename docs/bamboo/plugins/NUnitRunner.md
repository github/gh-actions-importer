# NUnitRunner

## Bamboo input

```yaml
 any-task:
   plugin-key: com.atlassian.bamboo.plugin.dotnet:nunitRunner
   configuration:
     include: test_category_to_exclude
     environmentVariables: VARIABLE="test"
     nunitResultsFile: TestResult.xml
     commandLineOptions: verbose
     run: tests_to_run
     exclude: test_category_to_exclude
     label: system.builder.nunit3.nuit
     nunitTestFiles: "foo..dll"
```

## Transformed Github Action

```yaml
- name: Run Unit Tests
  uses: microsoft/vstest-action@v1.0.0
- name: Publish NUnit test results
  uses: EnricoMi/publish-unit-test-result-action@v2.6.0
  if: always()
  with:
    nunit_files: "foo.dll"
```

## Unsupported Options

- include
- environmentVariables
- commandLineOptions
- run
- exclude
- label
