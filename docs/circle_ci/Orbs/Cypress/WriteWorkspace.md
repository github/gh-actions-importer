# CypressIo/Cypress Write Workspace

## CircleCI Input

```yaml
orbs:
  cypress: cypress-io/cypress@x.y
jobs:
  cypress-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - cypress/write_workspace
```

### Transformed Github Action

```yaml
- uses: actions/upload-artifact@v2
  with: 
    path: |-
      ~/project
      ~/.cache/Cypress
```

### Unsupported Options

- None
