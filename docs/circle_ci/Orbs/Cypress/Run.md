# CypressIo/Cypress Run

## CircleCI Input

```yaml
orbs:
  cypress: cypress-io/cypress@x.y
jobs:
  cypress-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - cypress/run:
          post-checkout:
            - restore_cache
          record: true
          debug: true
          parallel: true
          ci-build-id: '${{ github.sha }}-${{ github.workflow }}-${{ github.event_name }}'
          group: my-group
          build: npm run build
          start: npm start
          wait-on: 'http://localhost:8080'
          browser: firefox
          store_artifacts: true
          spec: --spec *
          install-command: yarn --frozen-lockfile --silent
          command: yarn cypress run-ct
          command-prefix: 'percy exec -- npx'
          cache-key: cache-{{ arch }}-{{ .Branch }}-{{ checksum "MY_CUSTOM_CACHE_KEY" }}
          working-directory: my/path
          timeout: 20m
          config-file: tests/cypress-config.json
          config: pageLoadTimeout=100000,baseUrl=http://localhost:3000
```

### Transformed Github Action

```yaml
- name: restore_cache
  uses: actions/cache@v2
  with:
    key: my-example-key
- uses: cypress-io/github-action@v2
  with:
    record: true
    config: pageLoadTimeout=100000,baseUrl=http://localhost:3000
    config-file: tests/cypress-config.json
    browser: firefox
    command: yarn cypress run-ct
    start: npm start
    build: npm run build
    install: true
    install-command: yarn --frozen-lockfile --silent
    wait-on: http://localhost:8080
    parallel: true
    group: my-group
    working-directory: my/path
    spec: "--spec *"
    command-prefix: percy exec -- npx
    ci-build-id: "${{ github.sha }}-${{ github.workflow }}-${{ github.event_name }}"
    cache-key: cache-{{ arch }}-{{ .Branch }}-{{ checksum "MY_CUSTOM_CACHE_KEY" }}
  timeout-minutes: '20'
  env:
    DEBUG: true
- uses: actions/upload-artifact@v2
  if: failure()
  with:
    name: cypress-screenshots
    path: cypress/screenshots
- uses: actions/upload-artifact@v2
  if: always()
  with:
    name: cypress-videos
    path: cypress/videos
```

### Unsupported Options

- post-install
- verify-command
- yarn
- attach-workspace
- no-workspace
- executor
