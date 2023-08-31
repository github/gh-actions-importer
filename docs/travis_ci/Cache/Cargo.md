# Cargo

## Travis Input

```yaml
cache:
  cargo: true
```

## Transformed Github Action

```yaml
- name: Set up cargo cache
  uses: actions/cache@v2
  with:
    path: |
      ~/.cargo/registry
      ~/.cargo/git
      target
    key: "${{ runner.os }}-cargo-${{ hashFiles('**/Cargo.lock') }}"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
