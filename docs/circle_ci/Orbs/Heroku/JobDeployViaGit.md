# CircleCI/Heroku Job Deploy Via Git

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@x.y
version: 2.1
workflows:
  heroku_deploy:
    jobs:
      - heroku/deploy-via-git:
          branch: main
          post-deploy:
            - run: 
                command: |
                  echo running your-database-migration-command
                  echo Done!
          pre-deploy:
            - run: echo command-that-run-before-deploying
```

### Transformed Github Action

```yaml
jobs:
  heroku_deploy-via-git:
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
      with:
        fetch-depth: 0
    - run: echo command-that-run-before-deploying
    - uses: "./.github/actions/heroku_deploy_via_git"
      timeout-minutes: 10
      with:
        api_key: "${{ secrets.HEROKU_API_KEY }}"
        branch: main
    - run: |
        echo running your-database-migration-command
        echo Done!
```

### Unsupported Options
- None
