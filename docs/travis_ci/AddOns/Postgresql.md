# Postgresql

## Travis Input

```yaml
addons:
  postgresql: "11"
```

### Transformed Github Action

```yaml
services:
  postgresql:
    image: postgres:11
```

### Unsupported Options

- None
