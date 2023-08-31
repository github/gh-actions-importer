# Restore Cache

## CircleCI Input

```yaml
steps:
  - restore_cache:
      keys: v1-myapp-{{ arch }}-{{ checksum "project.clj" }}
```

### Transformed Github Action

```yaml
- name: restore_cache
  uses: actions/cache@v2
  with:
    restore-keys: |-
      my-example-key
      my-other-key
```

### Unsupported Options

- `actions/cache@v2` is not supported on GHES
