# Pip

## Travis Input

```yaml
cache:
  pip: true
```

## Transformed Github Action

```yaml
- name: Set up pip cache
  uses: actions/cache@v2
  with:
    path: "~/.cache/pip"
    key: "${{ runner.os }}-pip-${{ hashFiles('**/requirements.txt') }}"
    restore_keys: "${{ runner.os }}-pip-"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
