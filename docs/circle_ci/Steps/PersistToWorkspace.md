# Persist To Workspace

## CircleCI Input

```yaml
steps:
- persist_to_workspace
    root: root
    paths:
      - foo/bar
      - baz
```

### Transformed Github Action

```yaml
- uses: actions/upload-artifact@v2
  with: 
    path: |-
      root/foo/bar
      root/baz
```

### Unsupported Options

- None
