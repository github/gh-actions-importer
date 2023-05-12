# Junit

## Bamboo input

```yaml
  - test-parser:
      type: junit
      ignore-time: false
      test-results: report.xml
```

## Transformed Github Action

```yaml
name: Publish test results
uses: EnricoMi/publish-unit-test-result-action@v2.6.0
with:
  junit_files: report.xml
```

## Unsupported Options
- description
- ignore-time
