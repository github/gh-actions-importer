# Npm

## Travis Input

```yaml
cache:
  npm: true
```

## Transformed Github Action

```yaml
- name: Set up npm cache
  uses: actions/cache@v2
  with:
    path: "~/.npm"
    key: "${{ runner.os }}-node-${{ hashFiles('**/package-lock.json') }}"
    restore-keys: "${{ runner.os }}-node-"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
