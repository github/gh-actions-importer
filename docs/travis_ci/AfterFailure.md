# After Failure

## Travis Input

```yaml
after_failure:
  - npm run lint
  - npm run build
```

### Transformed Github Action

```yaml
- run: npm run lint
  if: "${{ failure() }}"
```

### Supported Options

The scripts transformer will transform some of the TravisCI default environment variables automatically.
See [Scripts.md](Scripts.md) for the supported environment variables.
