# after_script

## GitLab input

```yaml
before_script:
  - echo "Execute this command after any `script:` commands."
```

### Transformed Github Action

```yaml
- run: echo "Execute this command after any `script:` commands."
  if: always()
```

### Unsupported Options

None
