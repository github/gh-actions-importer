# CypressIo/Cypress Setup

## CircleCI Input

```yaml
orbs:
  cypress: cypress-io/cypress@x.y
jobs:
  cypress-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - cypress/setup:
          post-install:
            - checkout
          build: npm run build
          install-command: yarn --frozen-lockfile --silent
          cache-key: cache-{{ arch }}-{{ .Branch }}-{{ checksum "MY_CUSTOM_CACHE_KEY" }}
          working_directory: my/path
          yarn: true
```

### Transformed Github Action

```yaml
- uses: actions/cache@v2
  with:
    key: my-cache-key
    path: "~/.cache"
- run: yarn --frozen-lockfile --silent
  working-directory: my/path     
- uses: actions/checkout@v2
- run: npm run build
  working-directory: my/path
```

### Unsupported Options

- executor
