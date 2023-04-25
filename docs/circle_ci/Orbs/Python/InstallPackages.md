# CircleCI/Python Install Packages

## CircleCI Input

```yaml
orbs:
  python: circleci/python@x.y
jobs:
  python-example:
    docker:
      - image: "cimg/base:stable"
    steps:
      - python/install-packages:
          args: "-f file:///path/to/archive/"
          pre-install-steps:
            - checkout
          app-dir: my-path
          pkg-manager: pipenv
          include-branch-in-cache-key: true
          include-python-in-cache-key: true
```

### Transformed Github Action

```yaml
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
```

### Unsupported Options

- cache-version
- `actions/cache@v2` is not supported on GHES
