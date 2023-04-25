# CircleCI/Windows Default Executor

## CircleCI Input

```yaml
orbs:
  win: circleci/windows@x.y
jobs:
  use-windows-example:
    executor: win/default
    steps:
      - checkout
```

### Transformed Github Action

```yaml
job:
  use-windows-example:
    runs-on: windows-2019
```

### Unsupported Options

- None
