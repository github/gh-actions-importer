# CypressIo/Cypress Install

## CircleCI Input

```yaml
orbs:
  cypress: cypress-io/cypress@x.y
jobs:
  cypress-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - cypress/install:
          post-install:
            - checkout
          build: npm run build
          install-command: yarn --frozen-lockfile --silent
          cache-key: cache-{{ arch }}-{{ .Branch }}-{{ checksum "MY_CUSTOM_CACHE_KEY" }}
          working_directory: my/path
          verify-command: npx verify-mycommand
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
- run: npx verify-mycommand
  working-directory: my/path
- run: npm run build
  working-directory: my/path
```

### Unsupported Options

- executor
