# Cocoapods

## Travis Input

```yaml
cache:
  cocoapods: true
```

## Transformed Github Action

```yaml
- name: Set up cocoapods cache
  uses: actions/cache@v2
  with:
    path: Pods
    key: "${{ runner.os }}-pods-${{ hashFiles('**/Podfile.lock') }}"
    restore-keys: "${{ runner.os }}-pods-"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
