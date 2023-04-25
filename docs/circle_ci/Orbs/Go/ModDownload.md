# CircleCI/Go Mod Download

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
      - go/mod-download
```

## Transformed Github Action

```yaml
run: go mod download
```

## Unsupported Options
