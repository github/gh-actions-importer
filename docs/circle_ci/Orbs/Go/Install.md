# CircleCI/Go Install

## CircleCI Input

```yaml
orbs:
  go: circleci/go@x.y
jobs:
  my_job:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - checkout
      - go/install:
          cache: true
          cache-key: gocache
          version: 1.17.0
```

## Transformed Github Action

```yaml
uses: actions/setup-go@v3
with:
  cache: true
  version: 1.17.0
```

## Unsupported Options

The `cache` and `cache-key` options are ignored. GitHub Actions caches tools automatically.
