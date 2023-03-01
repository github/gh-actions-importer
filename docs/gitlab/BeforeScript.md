# before_script

## GitLab input

```yaml
before_script:
  - echo "Execute this command before any `script:` commands."
```

### Transformed Github Action

```yaml
- run: echo "Execute this command before any `script:` commands."
```

### Unsupported Options

None
