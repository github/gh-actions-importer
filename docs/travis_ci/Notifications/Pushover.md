# Pushover

## Travis Input

```yaml
notifications:
  pushover:
    api_key:
      - "api_key_1"
```

## Transformed Github Action

```yaml
- uses: desiderati/github-action-pushover@v1
  with:
    job-status: ${{ job.status }}
    pushover-api-token: api_key_1
    pushover-user-key: ${{ secrets.PUSHOVER_USER_KEY }}
```

### Unsupported Options

- users
- template
