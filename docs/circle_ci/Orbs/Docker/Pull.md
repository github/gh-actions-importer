# CircleCI/Docker Pull

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/pull:
          images: my-image,my-other-image
          ignore-docker-pull-error: true
```

### Transformed Github Action

```yaml
- run: docker pull my-image
  continue-on-error: true
- run: docker pull my-other-image
  continue-on-error: true
```

### Unsupported Options

- None
