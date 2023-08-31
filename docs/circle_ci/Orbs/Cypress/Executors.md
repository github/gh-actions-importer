# CypressIo/Cypress Executors

## CircleCI Input

```yaml
orbs:
  cypress: cypress-io/cypress@x.y
jobs:
  cypress-example:
    executor: cypress/base-6
    steps:
      - cypress/run
```

### Transformed Github Action

```yaml
jobs:
  cypress-example:
    runs-on: ubuntu-latest
    container:
      image: cypress/base:6
    steps:
    - uses: actions/checkout@v2
```

## Supported Executors

- default
- base-6
- base-8
- base-10
- base-10-22-0
- base-12-6-0
- base-12-14-0
- base-12-16-1
- base-12-18-3
- base-12
- base-14-0-0
- base-14-7-0
- base-14
- browsers-chrome69
- browsers-chrome73
- browsers-chrome74
- browsers-chrome75
- browsers-chrome76
- browsers-chrome77
- browsers-chrome78-ff70
- browsers-chrome73-ff68
