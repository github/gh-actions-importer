# Yarn

## Travis Input

```yaml
cache:
  yarn: true
```

## Transformed Github Action

```yaml
- name: Get yarn cache directory path
  id: yarn-cache-dir-path
  run: echo "dir=$(yarn cache dir)" >> $GITHUB_OUTPUT
- name: Set up yarn cache
  uses: actions/cache@v2
  with:
    path: "${{ steps.yarn-cache-dir-path.outputs.dir }}"
    key: "${{ runner.os }}-yarn-${{ hashFiles('**/yarn.lock') }}"
    restore-keys: "${{ runner.os }}-yarn-"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
