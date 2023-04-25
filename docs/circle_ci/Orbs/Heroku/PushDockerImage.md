# CircleCI/Heroku Push Docker Image

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@1.2.6

jobs:
  example:
    executor: heroku/default
    steps:
      - checkout
      - heroku/push-docker-image:
          app-name: "tranquil-gopher-123"
          recursive: false
          process-types: web
          no_output_timeout: 1m
```

### Transformed Github Action

```yaml
- shell: bash
  run: |-
    heroku container:login
    heroku container:push -a tranquil-gopher-123 web
  timeout-minutes: 1
```

### Unsupported Options
- api-key (ignoring because it does not seem to be used by Orb Command)
