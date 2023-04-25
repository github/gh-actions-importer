# Services

## GitLab Input

```yaml
# Services can be configured at the top level, in defaults, or at the job level
services:
  - ruby:latest
  - postgres
  - name: redis:6.0
    alias: redis-alias
    entrypoint: ["docker-entrypoint.sh", "-b"]
    command: [start]
```

### Transformed Github Action

```yaml
services:
  ruby:latest:
    image: ruby:latest
  postgres:
    image: postgres
  redis-alias:
    image: redis:6.0
    options: "--entrypoint docker-entrypoint.sh redis:6.0 start"
```

### Unsupported Options

- entrypoint (Unable to convert entrypoints with arguments)
