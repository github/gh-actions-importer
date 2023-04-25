# CircleCI/Node Default Executor

## CircleCI Input

```yaml
orbs:
  node: circleci/node@x.y
jobs:
  install-node-example:
    executor: node/default
    steps:
      - checkout
```

### Transformed Github Action

```yaml
job:
  install-node-example:
    container:
      image: node
```

### Unsupported Options

- None
