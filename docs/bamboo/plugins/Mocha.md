# Mocha

## Bamboo input

```yaml
  - test-parser:
      type: mocha
      ignore-time: false
      test-results:
      - mocha-1.json
      - mocha-2.json
```

## Transformed Github Action

```yaml
- name: Publish Mocha test results
  uses: EnricoMi/publish-unit-test-result-action@v2.6.0
  if: env.test != '' && always()
  with:
    files: |-
      mocha-1.json
      mocha-2.json
```

## Unsupported Options

- ignore-time
- subdir