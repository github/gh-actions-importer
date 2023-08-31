# CircleCI/Node Test

## CircleCI Input

```yaml
orbs:
  node: circleci/node@x.y

workflows:
  my-workflow:
    jobs:
      node/test:
        with-cache: true
        pkg-manager: npm
        run-command: test
        setup:
          - run: echo "setup step!"
```

### Transformed Github Action

```yaml
- uses: actions/checkout@v2
- id: npm-cache-dir
  run: echo "dir=$(npm config get cache)" >> $GITHUB_OUTPUT
- uses: actions/cache@v2
  with:
    path: "${{ steps.npm-cache-dir.outputs.dir }}"
    key: "${{ runner.os }}-node-${{ hashFiles('**/package-lock.json') }}"
    restore-keys: "${{ runner.os }}-node-"
- run: npm run test
```

### Unsupported Options

- app-dir
- cache-version
- version
- `actions/cache@v2` is not supported on GHES
