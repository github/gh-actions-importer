# Branches

## Travis Input

```yaml
branches:
  only:
  - master
  except:
  - develop
```

### Transformed Github Action

```yaml
on:
  push:
    branches:
    - master
    - "!develop"
```

### Unsupported Options

- Only and except properties cannot be combined
