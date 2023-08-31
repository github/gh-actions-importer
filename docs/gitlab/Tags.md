# Tags

## GitLab Input

```yaml
tags:
  - windows # osx and linux map to macos-latest and ubuntu-latest

tags:
  - postgres
  - development
```

### Transformed Github Action

```yaml
runs-on:
  - windows-latest

runs-on:
  - self-hosted
  - postgres
  - development
```

### Unsupported Options

- None
