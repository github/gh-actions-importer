# Attach Workspace

## CircleCI Input

```yaml
steps:
- attach_workspace
    at: my/path
```

### Transformed Github Action

```yaml
- uses: actions/download-artifact@v2
  with: 
    path: my/path
```

### Unsupported Options

- None
