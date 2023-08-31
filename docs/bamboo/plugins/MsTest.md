# MSTest

## Bamboo input

```yaml
plugin-key: com.atlassian.bamboo.plugin.dotnet:mstest
configuration:
  mstestTestResultsDirectory: tests1\results.trx, tests2\results.trx
  pickupOutdatedFiles: 'false'
conditions:
- variable:
    exists: test
description: This is an important MS Test task
```

## Transformed Github Action

```yaml
- name: Publish MS Test results
  uses: EnricoMi/publish-unit-test-result-action@v2.6.0
  if: env.test != '' && always()
  with:
    files: |-
      tests1\results.trx
      tests2\results.trx
```

## Unsupported Options
- pickupOutdatedFiles
