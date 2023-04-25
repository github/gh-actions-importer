# CircleCI/AwsCli Default Executor

## CircleCI Input

```yaml
orbs:
  aws: circleci/aws-cli@x.y
jobs:
  install-aws-example:
    executor: aws/default
    steps:
      - checkout
```

### Transformed Github Action

```yaml
job:
  install-aws-example:
    container:
      image: cimg/python:3.9-node
```

### Unsupported Options

- None
