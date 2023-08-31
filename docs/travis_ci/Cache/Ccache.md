# Ccache

## Travis Input

```yaml
cache:
  ccache: true
```

## Transformed Github Action

```yaml
- name: Set up ccache cache
  uses: actions/cache@v2
  with:
    path: ".ccache"
    key: "${ { runner.os } }-ccache-${ { steps.ccache_cache_timestamp.outputs.timestamp } }"
    restore-keys: "${ { runner.os } }-ccache-"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
