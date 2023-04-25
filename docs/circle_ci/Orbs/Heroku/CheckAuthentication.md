# CircleCI/Heroku Check Authentication

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@1.2.6

jobs:
  example:
    executor: heroku/default
    steps:
      - checkout
      - heroku/check-authentication:
          print-whoami: false
```

### Transformed Github Action

```yaml
- shell: bash
  env:
    PRINT_WHOAMI: false
  run: |-
    if [[ $HEROKU_API_KEY == "" ]]; then
      echo "No Heroku API key set, please set the HEROKU_API_KEY environment variable."
      echo "This can be found by running the `heroku auth:token` command locally."
      exit 1
    else
      echo "Heroku API key found."
      if [[ $PRINT_WHOAMI == "true" ]]; then heroku auth:whoami; fi
    fi
```

### Unsupported Options
- None
