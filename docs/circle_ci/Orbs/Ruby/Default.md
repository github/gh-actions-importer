# CircleCI/Ruby Default Executor

## CircleCI Input

```yaml
orbs:
  ruby: circleci/ruby@x.y
jobs:
  install-ruby-example:
    executor: ruby/default
    steps:
      - checkout
```

### Transformed Github Action

```yaml
job:
  install-ruby-example:
    container:
      image: ruby:2.7
```

### Unsupported Options

- None
