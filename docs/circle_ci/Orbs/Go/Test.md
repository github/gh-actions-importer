# CircleCI/Go Test

## CircleCI Input

```yaml
orbs:
  go: circleci/go@x.y
jobs:
  docker:
    - image: 'cimg/base:stable'
  steps:
    - checkout
    - go/test:
        covermode: atomic
        failfast: true
        race: true
```

## Transformed Github Action

```yaml
- run: go test -covermode=atmoic -failfast -race
```

## Unsupported Options
