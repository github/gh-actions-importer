# CircleCI/Heroku Job Push Docker Image

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@x.y
version: 2.1
workflows:
  heroku_deploy:
    jobs:
      - heroku/push-docker-image:
          app-name: sprinkle-donut-123
          maintenance-mode: true
          post-deploy:
            - run: echo "running database migration command"
          pre-deploy:
            - run: echo "running pre-deploy command"
```

### Transformed Github Action

```yaml
jobs:
  heroku_push-docker-image:
    runs-on: ubuntu-latest
    steps:
    - shell: bash
      run: |-
        if [[ $(command -v heroku) == "" ]]; then
          curl https://cli-assets.heroku.com/install.sh | sh
        else
          echo "Heroku is already installed. No operation was performed."
        fi
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
    - uses: actions/checkout@v2
    - run: heroku maintenance:on --app sprinkle-donut-123
    - run: echo "running pre-deploy command"
    - shell: bash
      run: |-
        heroku container:login
        heroku container:push -a sprinkle-donut-123 web
      timeout-minutes: 10
    - shell: bash
      run: |-
        heroku container:login
        heroku container:release -a sprinkle-donut-123 web
      timeout-minutes: 10
    - run: echo "running database migration command"
    - run: heroku maintenance:off --app sprinkle-donut-123
```

### Unsupported Options
- None
