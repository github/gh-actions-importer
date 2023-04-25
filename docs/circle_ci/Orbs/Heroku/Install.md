# CircleCI/Heroku Install

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@1.2.6

jobs:
  example:
    executor: heroku/default
    steps:
      - checkout
      - heroku/install
```

### Transformed Github Action

```yaml
- shell: bash
  run: |-
    if [[ $(command -v heroku) == "" ]]; then
      curl https://cli-assets.heroku.com/install.sh | sh
    else
      echo "Heroku is already installed. No operation was performed."
    fi
```

### Unsupported Options
- None
