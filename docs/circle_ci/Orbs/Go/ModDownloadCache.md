# CircleCI/Go Mod Download Cache

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
      - go/mod-download-cache
```

## Transformed Github Action

```yaml
uses: actions/setup-go@v3
with:
  cache: true
run: go mod download
```

## Unsupported Options

Actions which use repository cache are not supported with GitHub Enterprise Server (GHES) 3.4 (`ghes-3.4`) or lower.

## Additional Details

This command is a shorthand for the following:

```yaml
- go/load-cache
- go/mode-download
- go/save-cache
```

Because these commands rely on the default Go path and a default cache key, the combined `load-cache` and `save-cache` functionality is replaced with `actions/setup-go` with `cache: true`. This provides the same behavior. See also [LoadCache](LoadCache.md), [SaveCache](SaveCache.md), and [ModDownload](ModDownload.md).
