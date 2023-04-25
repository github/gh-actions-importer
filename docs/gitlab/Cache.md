# Cache

## GitLab Input

```yaml
cache:
  key:
    files:
      - Gemfile.lock
      - package.json
  paths:
    - vendor/ruby
    - node_modules
```

### Transformed Github Action

```yaml
- uses: actions/cache@v2
  with:
    path: |-
      vendor/ruby
      node_modules
    key: "${{ runner.os }}-${{ hashFiles('Gemfile.lock', 'package.json') }}"
```

### Unsupported Options

- `untracked`
- `when`
- `actions/cache@v2` is not supported on GHES
