# CircleCI/Docker Build

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/build:
          attach-at: path-to-attach
          cache_from: image1, image2
          extra_build_args: --compress
          path: wd
          image: my-image
          lint-dockerfile: true
          treat-warnings-as-errors: true
          step-name: My docker build step
          tag: my-tag
          registry: my-registry
```

### Transformed Github Action

```yaml
- run: npm install -g dockerlint
- run: dockerlint -p Dockerfile
- uses: actions/download-artifact@v2
  with:
    path: path-to-attach
- run: docker pull image1
- run: docker pull image2
- name: My docker build step
  run: |-
    docker build \\
    --compress \\
    -f wd/Dockerfile \\
    --cache-from image1 \\
    --cache-from image2 \\
    -t my-registry/my-image:my-tag
  timeout-minutes: '10'
  continue-on-error: true
```

### Unsupported Options

- debug
