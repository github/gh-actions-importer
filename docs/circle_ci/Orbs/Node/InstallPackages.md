# CircleCI/Node Install Packages

## CircleCI Input

```yaml
orbs:
  node: circleci/node@x.y
jobs:
  install-node-example:
    docker:
      - image: "cimg/base:stable"
    steps:
      - checkout
      - node/install-packages:
          with-cache: true
          pkg-manager: yarn
          override-ci-command: my-custom-command
```

### Transformed Github Action

```yaml
- id: yarn-cache-dir-path
  run: echo "dir=$(yarn config get cacheFolder)" >> $GITHUB_OUTPUT
- uses: actions/cache@v2
  with:
    path: "${{ steps.yarn-cache-dir-path.outputs.dir }}"
    key: "${{ runner.os }}-yarn-${{ hashFiles('**/yarn.lock') }}"
    restore-keys: "${{ runner.os }}-yarn-"
- run: my-custom-command
```

### Unsupported Options

- app-dir
- cache-path
- cache-version
- include-branch-in-cache-key
- caching with yarn-berry is transformed using regular yarn
- `actions/cache@v2` is not supported on GHES
