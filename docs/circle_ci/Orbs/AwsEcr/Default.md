# CircleCI/aws-ecr Default Executor

## CircleCI Input

```yaml
orbs:
  aws-ecr: circleci/aws-ecr@x.y
jobs:
  use-aws-ecr-example:
    executor: aws-ecr/default
    steps:
      - checkout
```

### Transformed Github Action

```yaml
job:
  ubuntu-job:
    runs-on: ubuntu-20.04
```

### Unsupported Options

- use-docker-layer-caching
