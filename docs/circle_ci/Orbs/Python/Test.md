# CircleCI/Python Test

## CircleCI Input

```yaml
orbs:
  python: circleci/python@x.y
jobs:
  python-example:
    docker:
      - image: "cimg/base:stable"
    steps:
      - python/test:
          setup:
            - checkout
          app-dir: my-path
          pkg-manager: pipenv
```

### Transformed Github Action

```yaml
- uses: actions/checkout@v2
- name: Get python version
  id: python-version
  run: echo "python-version=$(python -v)" >> $GITHUB_OUTPUT
- uses: actions/cache@v2
  with:
    key: "${{ runner.os }}-${{ github.ref }}-python-${{ steps.python-version.outputs.python-version }}-pipenv-${{ hashFiles('Pipfile.lock') }}"
- uses: actions/checkout@v2
- name: Install dependencies with pipenv using project Pipfile or inline packages
  run: pipenv install -f file:///path/to/archive/
  working-directory: my-path
- name: Run tests with pipenv run
  run: pipenv run python -m unittest
- uses: actions/upload-artifact@v2
  with:
    path: test-report
```

### Unsupported Options

- cache-version
- version
- `actions/cache@v2` is not supported on GHES
