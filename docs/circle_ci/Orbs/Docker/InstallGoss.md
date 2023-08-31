# CircleCI/Docker Install Goss

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/install-goss:
          install-dir: my/path
          version: v1.0
```

### Transformed Github Action

```yaml
- run: curl -fsSL https://goss.rocks/install | GOSS_DST='my/path' GOSS_VER=v1.0 sh
```

### Unsupported Options

- debug
