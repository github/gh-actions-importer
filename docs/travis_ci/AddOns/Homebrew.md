# Homebrew

## Travis input

```yaml
homebrew:
  packages:
  - cmake
```

### Transformed Github Action

```yaml
- run: |-
    brew install cmake
```

### Unsupported Options

- brewfile
