# CircleCI/Node Run

## CircleCI Input

```yaml
orbs:
  node: circleci/node@x.y

workflows:
  my-workflow:
    jobs:
      node/run:
        with-cache: true
        pkg-manager: npm
        npm-run: npm-script
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
- run: npm run npm-script
```

### Unsupported Options

- app-dir
- cache-version
- version
- `actions/cache@v2` is not supported on GHES
