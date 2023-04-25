# After Script

## GitLab Input

```yaml
after_script:
  - echo "Execute this command after any `script:` commands."
```

### Transformed Github Action

```yaml
- run: echo "Execute this command after any `script:` commands."
  if: always()
```

### Unsupported Options

None
