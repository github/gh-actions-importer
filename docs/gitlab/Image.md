# Image

## GitLab Input

```yaml
# String configuration
image: ruby:latest

# Multiple parameter configuration
image:
  name: postgres
  entrypoint: ["docker-entrypoint.sh", "-b"]
```

### Transformed Github Action

```yaml
# String output
container:
  image: ruby:latest

# Multiple parameter output
container:
  image: postgres
  options: "--entrypoint docker-entrypoint.sh"
```

### Unsupported Options

- entrypoint (Unable to convert entrypoints with arguments)
