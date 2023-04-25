# CircleCI/Ruby Rubocop Check

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
      - ruby/rubocop-check:
          out-path: "my-out-path"
          check-path: "my-check-path"
          label: "My linting run"
```

### Transformed Github Action

```yaml
- name: My linting run
  run: bundle exec rubocop my-check-path --format progress
```

### Unsupported Options

- out-path
