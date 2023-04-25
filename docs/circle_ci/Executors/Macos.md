# Macos

## CircleCI Input

```yaml
macos:
  xcode: "1.2.3"
```

### Transformed Github Action

```yaml
runs-on: macos-latest
steps:
    - uses: maxim-lobanov/setup-xcode@v1
      with:
        xcode-version: 1.2.3
```

### Unsupported Options

- None
