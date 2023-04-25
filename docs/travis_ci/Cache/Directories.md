# Directories

## Travis Input

```yaml
cache:
  directories:
    - "path"
```

## Transformed Github Action

```yaml
- name: Set up cache
  uses: actions/cache@v2
  with:
    path: |-
      $HOME/bin
      path
    key: "${{ runner.os }}-path }}"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
