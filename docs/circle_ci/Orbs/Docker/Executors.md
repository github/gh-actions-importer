# CircleCI/Docker Executors

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    executor: 
      name: docker/docker
      image: python
      tag: 2

    steps:
      - checkout
```

### Transformed Github Action

```yaml
- container:
    image: python:2
```

## Supported Executors

- hadolint
- docker
- machine

### Unsupported Options

- None
