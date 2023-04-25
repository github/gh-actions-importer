# CircleCI/Docker Configure Docker Credentials Store

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/configure-docker-credentials-store:
          helper-name: pass
          docker-config-path: $HOME/.docker/config.json
```

### Transformed Github Action

```yaml
- run: |-
    mkdir -p $(dirname $HOME/.docker/config.json)
    cat $HOME/.docker/config.json | jq --arg credsStore '${{ env.HELPER_NAME }}' '. + {credsStore: $credsStore}' > /tmp/docker-config-credsstore-update.json
    cat /tmp/docker-config-credsstore-update.json > $HOME/.docker/config.json
    rm /tmp/docker-config-credsstore-update.json
  env:
    HELPER_NAME: pass
```

### Unsupported Options

- None
