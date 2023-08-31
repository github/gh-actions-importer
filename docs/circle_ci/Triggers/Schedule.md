# Schedule

## CircleCI Input

```yaml
triggers: # use the triggers key to indicate a scheduled build
  - schedule:
      cron: 51 3 * * *
```

### Transformed Github Action

```yaml
on:
  schedule:
  - cron: "51 3 * * *"
```

### Unsupported Options

- Branch filters
