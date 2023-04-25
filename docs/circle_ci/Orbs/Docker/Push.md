# CircleCI/Docker Push

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/push:
          image: my-image
          tag: mytag1,mytag2
          registry: my-registry
          step-name: Docker Push Step
```

### Transformed Github Action

```yaml
- name: Docker Push Step
- run: docker push my-registry/my-image:mytag1
- name: Docker Push Step
- run: docker push my-registry/my-image:mytag2
```

### Unsupported Options

- digest-path
