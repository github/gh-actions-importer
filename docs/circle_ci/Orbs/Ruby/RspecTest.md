# CircleCI/Ruby Rspec Test

## CircleCI Input

```yaml
orbs:
  node: circleci/ruby@x.y
jobs:
  rspec-ruby-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - checkout
      - ruby/rspec-test:
          out-path: "my/path"
          label: "My test run"
```

### Transformed Github Action

```yaml
- name: My test run
  run: bundle exec rspec spec --profile 10 --format RspecJunitFormatter --out my/path/results.xml --format progress
```

### Unsupported Options

- None
