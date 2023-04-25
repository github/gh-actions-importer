# Save Cache

## CircleCI Input

```yaml
steps:
  - save_cache:
      key: v1-myapp-{{ arch }}-{{ checksum "project.clj" }}
      paths:
        - /home/ubuntu/.m2
```

### Transformed Github Action

```yaml
- uses: actions/cache@v2
  with:
    path: /home/ubuntu/.m2
    key: v1-myapp-{{ arch }}-{{ checksum "project.clj" }}
```

### Unsupported Options

- `actions/cache@v2` is not supported on GHES
- If a workflow contains both a save_cache and restore_cache step with the same key, they will be combined in the final Github workflow under the name restore_cache.
