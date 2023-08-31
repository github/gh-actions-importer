# Store Artifacts

## CircleCI Input

```yaml
steps:
- store_artifacts
    path: my-artifact
```

### Transformed Github Action

```yaml
- uses: actions/upload-artifact@v2
  with: 
    path: my-artifact
```

### Unsupported Options

- Destination
