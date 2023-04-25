# Script

## Travis Input

```yaml
deploy:
  provider: script
  script: bash scripts/deploy.sh
  on:
    branch: develop
```

## Transformed Github Action

```yaml
- run: bash scripts/deploy.sh
  if: "${{ github.event_name != 'pull_request' && github.ref == 'refs/heads/develop' }}"
```

### Unsupported Options

- skip cleanup (deprecated in Travis)
