# CircleCI/Docker Hadolint

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/hadolint:
          dockerfiles: Dockerfile, MyDockerfile
          ignore-rules: DL3000, SC1010
```

### Transformed Github Action

```yaml
- uses: hadolint/hadolint-action@v3.1.0
  with:
    dockerfile: Dockerfile
    ignore: DL3000 SC1010
- uses: hadolint/hadolint-action@v3.1.0
  with:
    dockerfile: MyDockerfile
    ignore: DL3000 SC1010
```

### Unsupported Options

- trusted-registries
