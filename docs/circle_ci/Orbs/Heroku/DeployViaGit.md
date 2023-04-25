# CircleCI/Heroku Deploy Via Git

## CircleCI Input

```yaml
orbs:
  heroku: circleci/heroku@1.2.6

jobs:
  example:
    executor: heroku/default
    steps:
      - heroku/deploy-via-git:
          api-key: HEROKU_API_KEY
          app-name: "gentle-yeti-42"
          branch: dev
```

### Transformed Github Action(Composite Action Feature: enabled)
workflow
```yaml
- uses: "./.github/actions/heroku_deploy_via_git"
  timeout-minutes: 10
  with:
    api_key: "${{ secrets.HEROKU_API_KEY }}"
    app_name: gentle-yeti-42
    branch: dev
```

action.yml
```yaml
name: heroku_deploy_via_git
inputs:
  api_key:
    required: true
  app_name:
    required: false
    default: "$HEROKU_APP_NAME"
  branch:
    required: false
    default: "${{ github.ref }}"
  force:
    required: false
    default: false
  maintenance_mode:
    required: false
    default: false
  tag:
    required: false
    default: "${{ github.ref }}"
runs:
  using: composite
  steps:
  - shell: bash
    env:
      HEROKU_API_KEY: "${{ inputs.api_key }}"
      BRANCH: "${{ inputs.branch }}"
      TAG: "${{ inputs.tag }}"
      FORCE: "${{ inputs.force }}"
      APP_NAME: "${{ inputs.app_name }}"
      MAINTENANCE_MODE: "${{ inputs.maintenance_mode }}"
    run: |-
      if ${{ env.FORCE }};then force="-f"; fi
      heroku_url="https://heroku:${{ env.HEROKU_API_KEY }}@git.heroku.com/${{ env.APP_NAME }}.git"

      if ${{ env.MAINTENANCE_MODE }}; then heroku maintenance:on --app ${{ env.APP_NAME }}; fi
      if [ -n "${{ env.BRANCH }}" ]; then
        git push $force $heroku_url ${{ env.BRANCH }}:main
      elif [ -n "${{ env.TAG }}" ]; then
        git push $force $heroku_url ${{ env.TAG }}^{}:main
      else
        echo "No branch or tag found."
      fi
      if ${{ env.MAINTENANCE_MODE }}; then heroku maintenance:off --app ${{ env.APP_NAME }}; fi
```

### Transformed Github Action(Composite Action Feature: disabled)
```yaml
- shell: bash
  env:
    HEROKU_API_KEY: "${{ secrets.HEROKU_API_KEY }}"
    BRANCH: dev
    TAG: "${{ github.ref }}"
    FORCE: false
    APP_NAME: gentle-yeti-42
    MAINTENANCE_MODE: false
  run: |-
    if ${{ env.FORCE }};then force="-f"; fi
    heroku_url="https://heroku:${{ env.HEROKU_API_KEY }}@git.heroku.com/${{ env.APP_NAME }}.git"
    if ${{ env.MAINTENANCE_MODE }}; then heroku maintenance:on --app ${{ env.APP_NAME }}; fi
    if [ -n "${{ env.BRANCH }}" ]; then
      git push $force $heroku_url ${{ env.BRANCH }}:main
    elif [ -n "${{ env.TAG }}" ]; then
      git push $force $heroku_url ${{ env.TAG }}^{}:main
    else
      echo "No branch or tag found."
    fi
    if ${{ env.MAINTENANCE_MODE }}; then heroku maintenance:off --app ${{ env.APP_NAME }}; fi
  timeout-minutes: 10
```


### Unsupported Options
- None
