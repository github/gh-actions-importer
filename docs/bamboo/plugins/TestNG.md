# TestNG

## Bamboo input

```yaml
- test-parser:
    type: testng
    ignore-time: false
    test-results: "**/testng-results.xml"
    conditions:
      - variable:
          exists: FOO
```

## Transformed Github Action

```yaml
# This action will fail to process `testng-results.xml`, but will successfully handle TestNG test suite file(s), e.g. `**/TEST-*.xml`.
# Ensure the path to your test suite files(s) is correct. If you wish to use the original path, set `report_path` to "**/testng-results.xml".
- name: Publish TestNG results
  uses: scacap/action-surefire-report@v1.7.0
  with:
    report_paths: UPDATE_ME
  if: env.FOO != ''
```

## Unsupported Options

- `ignore-time`
