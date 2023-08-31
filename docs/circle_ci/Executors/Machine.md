# Machine

## CircleCI Input

```yaml
machine:
  - image: "ubuntu-2004:202104-01"
```

### Transformed Github Action

```yaml
runs-on: ubuntu-20.04
```

### Unsupported Options

- docker_layer_caching
- resource_class
