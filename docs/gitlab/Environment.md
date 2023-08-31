# Environment

## GitLab Input

```yaml
environment: deploy_environment
```

```yaml
environment:
  name: production_env
  url: https://prod.example.com
  # Unsupported
  action: stop
  on_stop: stop_review_app
  auto_stop_in: 1 day
  deployment_tier: production
  kubernetes:
    namespace: production
```

### Transformed Github Action

```yaml
environment: deploy_environment
```

```yaml
environment:
  name: production_env
  url: https://prod.example.com
```

### Unsupported Options

- on_stop
- action
- auto_stop_in
- kubernetes
- deployment_tier
