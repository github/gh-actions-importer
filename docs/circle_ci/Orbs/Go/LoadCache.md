# CircleCI/Go Load Cache

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
      - go/load-cache
```

## Transformed Github Action

```yaml
uses: actions/setup-go@v3
with:
  cache: true
```

## Unsupported Options

Actions which use repository cache are not supported with GitHub Enterprise Server (GHES) 3.4 (`ghes-3.4`) or lower.

If a `key` is provided and a matching `go/save-cache` command cannot be found in the same workflow, a manual task will be created to provide a path for the generated `actions/cache` step. There are three options for configuring this:

- Replace the placeholder string `''` with the path to the Go modules cache
- If the default key and Go module cache can be used, the `actions/cache` step can be removed. Update the `actions/setup-go` step to enable the cache by changing the value to `cache: true`.
- If a named key is needed and you need that to dynamically resolve to the Go modules cache, replace both the `actions/setup-go` step and `actions/cache` step with the following:

  ```yaml
  - uses: "./.github/actions/go_cache"
    with:
      key: your-key
  ```

## Additional Details

Valet will automatically combine and remove any related `go/save-cache` steps in the same job. Valet will also remove any additional `go/load-cache` steps with the same key. The full details are provided with [SaveCache](SaveCache.md).
