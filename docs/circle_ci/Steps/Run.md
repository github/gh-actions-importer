# Run

## CircleCI Input

```yaml
run:
  - command: make test
  - when: always
```

### Transformed Github Action

```yaml
- run: make test
  if: always()
```

### Unsupported Options

- background
- no_output_timeout
