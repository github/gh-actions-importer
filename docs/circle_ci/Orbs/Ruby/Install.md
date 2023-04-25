# CircleCI/Ruby Install

## CircleCI Input

```yaml
orbs:
  node: circleci/ruby@x.y
jobs:
  install-ruby-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - checkout
      - ruby/install:
          version: 3.2.1
      - run: ruby --version
```

### Transformed Github Action

```yaml
uses: ruby/setup-ruby@v1.138.0
with:
  ruby-version: 3.2.1
```

### Unsupported Options

- None
