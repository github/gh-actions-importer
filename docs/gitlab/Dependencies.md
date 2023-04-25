# Dependencies

## GitLab Input

```yaml
dependencies:
  - build:osx
```

### Transformed Github Action

```yaml
- uses: actions/download-artifact@v2
  with:
    name: build:osx
```

### Unsupported Options

None
