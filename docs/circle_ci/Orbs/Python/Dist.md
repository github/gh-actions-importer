# CircleCI/Python Dist

## CircleCI Input

```yaml
orbs:
  python: circleci/python@x.y
jobs:
  python-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - python/dist:
          app-dir: my-path
```

### Transformed Github Action

```yaml
- run: pip install wheel
- name: Build distribution package
  run: |-
    python setup.py sdist
    python setup.py sdist
    ls -l dist
  working-directory: my-path
```

### Unsupported Options

- install-yarn
- install-npm
- node-install-dir
- npm-version
- yarn-version
- lts
