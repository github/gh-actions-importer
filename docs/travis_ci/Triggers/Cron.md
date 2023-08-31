# Cron

## Travis Input

```json
{
  "settings": {
    "interval": "daily"
  }
}
```

### Transformed Github Action

```yaml
on:
  schedule:
    cron: "51 3 * * *"
```

### Unsupported Options

- Crons are only transformed for the default branch
