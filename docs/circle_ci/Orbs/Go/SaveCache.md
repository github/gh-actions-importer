# CircleCI/Go Save Cache

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

## Additional Details

The cache used by GitHub Actions is immutable, similar to CircleCI. The behavior of the caching Actions (such as `actions/cache`) are different.
CircleCI uses an explicit `go/load-cache` to retrieve the cache and `go/save-cache` to cache Go modules in a specific path.
GitHub Actions requires a single cache step to do both. The cache download happens during the `actions/cache` step, and the creation of a cache entry from the provided path happens automatically after the job completes.
Because of this difference, Valet automatically merges `load-cache` and `save-cache` steps with matching keys within a job.
The first `load-cache` with each key will be converted. If no `load-cache` exists in the job, the first `save-cache` for each key in the job will be converted.

The default `key` used by CircleCI is `go-mod` and the default Go modules cache `path` is `/home/circleci/go/pkg/mod` (which is also the `GOMODCACHE` for CircleCI).
If no `key` or `path` is provided (or if the default values are used), Valet will use `actions/setup-go` with `cache: true`. This provides the an equivalent behavior.

If a key or path is provided, Valet will create steps that manually configure the cache. It will use `actions/setup-go` with `cache: false` to configure the Go environment without configuring the cache.
It will then add an additional `actions/cache` step using the provided key and path. Because this step combines the load and save behaviors, it requires both a `key` and a `path`.

If a `key` is used, Valet will search for a matching `go/save-cache` to resolve the `path`. It will first look within the existing job for a match. If no match is found, it will examine all jobs in the workflow.
If a match is still not found, a manual task will be created to configure the `path` for `actions/cache`. More details can be found in [LoadCache](LoadCache.md).

When `key` is provided without a `path`, Valet will create a composite action (`.github/actions/go_cache`) to dynamically resolve the `GOMODCACHE` location.
Unlike CircleCI, this location is not a fixed path, and it can vary between versions of Go. The provided script has been tested with Go 1.9 - 1.19.

CircleCI uses the provided key as the prefix for the actual cache key. The equivalent expression for the `actions/cache` key is `${{ inputs.key }}-${{ runner.arch }}-${{ hashFiles( 'go.sum' ) }}`.
As a best practice, it is generally recommended to recursively hash all of the available `go.sum` files.
Valet implements this recommendation and uses the key `${{ inputs.key }}-${{ runner.arch }}-${{ hashFiles( '**/go.sum' ) }}`.
If the old behavior is required, the key can be manually updated after the conversion.

The implemented concersions:

| `load-cache` exists | `load-cache` `key` | `save-cache` exists | `save-cache` `key` | `save-cache` `path` | `actions/setup-go` | `actions/cache`                 | Composite Action               | Manual Task                         |
| ------------------- | ------------------ | ------------------- | ------------------ | ------------------- | ------------------ | ------------------------------- | ------------------------------ | ----------------------------------- |
| Yes                 | default            | No                  | -                  | -                   | `cache:true`       | No                              | No                             | No                                  |
| Yes                 | user-provided      | No                  | -                  | -                   | `cache:false`      | Provided `key`, empty `path`    | No                             | Configure `path` in `actions/cache` |
| No                  | -                  | Yes                 | default            | default             | `cache:true`       | No                              | No                             | No                                  |
| No                  | -                  | Yes                 | default            | user-provided       | `cache:false`      | Default `key`, provided `path`  | No                             | No                                  |
| No                  | -                  | Yes                 | user-provided      | default             | No                 | No                              | Yes. Provided `key`, no `path` | No                                  |
| No                  | -                  | Yes                 | user-provided      | user-provided       | `cache:false`      | Provided `key`, provided `path` | No                             | No                                  |
| Yes                 | user-provided      | Yes                 | from `load-cache`  | default             | No                 | No                              | Yes. Provided `key`, no `path` | No                                  |
| Yes                 | user-provided      | Yes                 | from `load-cache`  | user-provided       | `cache:false`      | Provided `key`, provided `path` | No                             | No                                  |
| Yes                 | default            | Yes                 | from `load-cache`  | default             | `cache:true`       | No                              | No                             | No                                  |
| Yes                 | default            | Yes                 | from `load-cache`  | user-provided       | `cache:false`      | Default `key`, provided `path`  | No                             | No                                  |
