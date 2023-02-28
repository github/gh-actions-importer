# Unless

## CircleCI input

```yaml
unless:
  condition:
    and: [true, true, false]
  steps:
    run: echo "my condition passed!"
```

### Transformed Github Action

```yaml
- run: echo "my condition passed!"
  if: "${{ !(true && true && false) }}"
```

### Unsupported Options

- none
