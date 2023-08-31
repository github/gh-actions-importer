# CircleCI/Python Default Executor

## CircleCI Input

```yaml
orbs:
  python: circleci/python@x.y
jobs:
  install-python-example:
    executor: python/default
    steps:
      - checkout
```

### Transformed Github Action

```yaml
job:
  install-python-example:
    container:
      image: python
```

### Unsupported Options

- None
