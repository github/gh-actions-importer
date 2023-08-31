# After Success

## Travis Input

```yaml
after_success:
  - echo "hello world!"
```

### Transformed Github Action

```yaml
- run: echo "hello world!"
  if: "${{ success() }}"
```

### Supported Options

The scripts transformer will transform some of the TravisCI default environment variables automatically.
See [Scripts.md](Scripts.md) for the supported environment variables.
