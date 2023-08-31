# CircleCI/Heroku Release Docker Image

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@1.2.6

jobs:
  example:
    executor: heroku/default
    steps:
      - checkout
      - heroku/release-docker-image:
          app-name: "green-dinosaur"
          process-types: db
          no_output_timeout: 11m
```

### Transformed Github Action

```yaml
- shell: bash
  run: |-
    heroku container:login
    heroku container:release -a green-dinosaur db
  timeout-minutes: 11
```

### Unsupported Options
- api-key (ignoring because it does not seem to be used by Orb Command)
