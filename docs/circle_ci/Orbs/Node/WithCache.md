# CircleCI/Node With Cache

## CircleCI Input

```yaml
orbs:
  node: circleci/node@x.y
jobs:
  install-node-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - checkout
      - node/with-cache:
        - run: npm install
        - run: 
            name: Run Test
            run: npm test
```

### Transformed Github Action

```yaml
- uses: actions/setup-node@v3
  with:
    node-version: 16
    cache: npm
- run: npm install
- name: Run Test
  run: npm test
```

### Unsupported Options

