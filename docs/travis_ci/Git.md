# Git

## Travis Input

```yaml
git:
  submodules: true
  depth: 1
```

## Transformed Github Action

```yaml
- uses: actions/checkout@v2
  with:
    submodules: true
    fetch-depth: 1
```

### Unsupported Options

- quiet
- strategy
- submodules_depth
- lfs_skip_smudge
- sparse_checkout
- autocrlf
