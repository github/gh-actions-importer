# CircleCI/Node Install

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
      - node/install:
          node-version: 3
      - run: node --version
```

### Transformed Github Action

```yaml
uses: actions/setup-node@v2
with:
  node-version: 3
```

### Unsupported Options

- install-yarn
- install-npm
- node-install-dir
- npm-version
- yarn-version
- lts
