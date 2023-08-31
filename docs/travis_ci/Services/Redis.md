# Redis

## Travis Input

```yaml
services:
  - redis
```

## Transformed Github Action

```yaml
services: 
  redis:
    image: redis
    options: --health-cmd "redis-cli ping" --health-interval 10s --health-timeout 5s --health-retries 5
```

### Unsupported Options

- None
