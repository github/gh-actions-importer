# CircleCI/Ruby Install Deps

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
      - ruby/install-deps:
          with-cache: true
          bundler-version: 1.2.3
          app-dir: my-working-dir
```

### Transformed Github Action

```yaml
- run: bundle check || bundle install
```

The with-cache parameter is transformed by updating any existing setup-ruby steps with `bundler-cache: true`, or by adding a new setup-ruby step to the workflow with the correct inputs.

```yaml
- uses: ruby/setup-ruby@v1.138.0
  with:
    ruby-version: 2.6
    bundler-cache: true
    bundler: 1.2.3
    working-directory: my-working-dir
- run: bundle check || bundle install
```

### Unsupported Options

- Key
- Path/App_dir will be ignored if caching is set
