# CircleCI/Heroku Default Executor

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@1.2.6
jobs:
  example:
    executor: heroku/default
    steps:
      - checkout
```

### Transformed Github Action

```yaml
jobs:
  example:
    runs-on: ubuntu-latest
    container:
      image: cimg/base:stable
      options: "--user root"
```

### Unsupported Options

- None
