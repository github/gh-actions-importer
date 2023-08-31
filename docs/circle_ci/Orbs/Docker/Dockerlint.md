# CircleCI/Docker Dockerlint

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/dockerlint:
          dockerfile: MyDockerfile
          treat-warnings-as-errors: true
```

### Transformed Github Action

```yaml
- run: npm install -g dockerlint
- run: dockerlint -p MyDockerfile
```

### Unsupported Options

- debug
