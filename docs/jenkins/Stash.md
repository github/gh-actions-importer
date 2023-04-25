# Stash

## Designer Pipeline

This plugin is not supported in Designer Pipelines.

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
  stash name:'Perimeter', includes: 'path/output/bin/, path/output/test-results', excludes:'path/**/*.tmp'
}
```

### Transformed Github Action

```yaml
name: stash
uses: actions/upload-artifact@v2
with:
  name: Perimeter
  path: |
    path/output/bin/
    path/output/test-results
    !path/**/*.tmp
    if-no-files-found: "fail"
```

### Unsupported Options

- None

### Notes

By default Jenkins pipeline will fail if the path to stash is empty Unless `allowEmpty` is set to `true`, while Actions will just throw a warning.
To preserve this behavior, if `allowEmpty` is not added to the Jenkinsfile, we add `if-no-files-found: "fail"` to the Actions workflow
