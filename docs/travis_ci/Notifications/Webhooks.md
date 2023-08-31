# Webhooks

## Travis Input

```yaml
notifications:
  webhooks:
    urls:
      - "www.example.com"
```

## Transformed Github Action

```yaml
- uses: distributhor/workflow-webhook@v3.0.2
  env:
    webhook_url: www.example.com
    webhook_secret: "${{ secrets.WEBHOOK_SECRET }}"
```

### Unsupported Options

- on_start
- on_cancel
- on_error
