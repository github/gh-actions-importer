# NUnit Parser

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugin.dotnet:nunit
    configuration:
      testResultsDirectory: "**/test-reports/*.xml"
      pickupOutdatedFiles: "false"
    conditions:
      - variable:
          exists: FOO
    description: Sample NUnit Parser task
```

## Transformed Github Action

```yaml
- name: Sample NUnit Parser task
  uses: EnricoMi/publish-unit-test-result-action@v2.6.0
  if: env.FOO != '' && always()
  with:
    nunit_files: "**/test-reports/*.xml"
```

## Unsupported Options

- `pickupOutdatedFiles`
  - This option will include test results created outside of the current build.
