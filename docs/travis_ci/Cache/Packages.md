# Packages

## Travis Input

```yaml
cache:
  packages: true
```

## Transformed Github Action

```yaml
- name: Set up R cache
  uses: actions/cache@v2
  with:
    path: "~/.local/share/renv"
    key: "${{ runner.os }}-renv-${{ hashFiles('**/renv.lock') }}"
    restore-keys: "${{ runner.os }}-renv-"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
