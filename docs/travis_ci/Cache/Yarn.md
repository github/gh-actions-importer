# Yarn

## Travis input

```yaml
cache:
  yarn: true
```

## Transformed Github Action

```yaml
- name: Get yarn cache directory path
  id: yarn-cache-dir-path
  run: echo '::set-output name=dir::$(yarn cache dir)'
- name: Set up yarn cache
  uses: actions/cache@v2
  with:
    path: "${{ steps.yarn-cache-dir-path.outputs.dir }}"
    key: "${{ runner.os }}-yarn-${{ hashFiles('**/yarn.lock') }}"
    restore-keys: "${{ runner.os }}-yarn-"
```

## Unsupported Options

- `actions/cache@v2` is not supported on GHES
