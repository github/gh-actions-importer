# Store Test Results

## CircleCI input

```yaml
steps:
- store_test_results
    path: my-artifact
```

### Transformed Github Action

```yaml
- uses: actions/upload-artifact@v2
  with: 
    path: my-artifact
```

### Unsupported Options

- None
