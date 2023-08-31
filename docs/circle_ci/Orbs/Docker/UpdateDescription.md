# CircleCI/Docker Update Description

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/update-description:
          docker-password: $DOCKER_PASS
          docker-username: $DOCKER_LOGIN
          path: /user/local
          readme: MYREADME.md
          registry: docker.io
          image: my-image
```

### Transformed Github Action (Composite Action Feature: enabled)
workflow
```yaml
- uses: "./.github/actions/docker_update_description"
  with:
    docker-password: "${{ secrets.DOCKER_PASSWORD }}"
    docker-username: "${{ env.DOCKER_USERNAME }}"
    image: my-image
    description-path: "/user/local/MYREADME.md"

```

action yaml
```yaml
name: docker_update_description
inputs:
  docker-password:
    required: true
  docker-username:
    required: true
  image:
    required: true
  description-path:
    required: false
    default: "./README.md"
runs:
  using: composite
  steps:
  - run: |-
      PAYLOAD='username=${{ env.DOCKER_USERNAME }}&password=${{ env.DOCKER_PASSWORD }}'
      JWT=$(curl -s -d $PAYLOAD https://hub.docker.com/v2/users/login/ | jq -r .token)
      HEADER="Authorization: JWT $JWT"
      URL='https://hub.docker.com/v2/repositories/${{ env.IMAGE }}/'
      STATUS=$(curl -s -o /dev/null -w %{http_code} -X PATCH -H "$HEADER" --data-urlencode full_description@$DESCRIPTION_PATH $URL)
      if [ $STATUS -ne 200 ]; then
        echo 'Could not update image description'
        echo "Error code: $STATUS"
        exit 1
      fi
    env:
      DOCKER_USERNAME: "${{ inputs.docker-username }}"
      DOCKER_PASSWORD: "${{ inputs.docker-password }}"
      IMAGE: "${{ inputs.image }}"
      DESCRIPTION_PATH: "${{ inputs.description-path }}"
    shell: bash
```
### Transformed Github Action (Composite Action Feature: disabled)
```yaml
- run: |-
    PAYLOAD='username=${{ env.DOCKER_USERNAME }}&password=${{ env.DOCKER_PASSWORD }}'
    JWT=$(curl -s -d $PAYLOAD https://hub.docker.com/v2/users/login/ | jq -r .token)
    HEADER="Authorization: JWT $JWT"
    URL='https://hub.docker.com/v2/repositories/${{ env.IMAGE }}/'
    STATUS=$(curl -s -o /dev/null -w %{http_code} -X PATCH -H "$HEADER" --data-urlencode full_description@$DESCRIPTION_PATH $URL)
    if [ $STATUS -ne 200 ]; then
      echo 'Could not update image description'
      echo "Error code: $STATUS"
      exit 1
    fi
  env:
    DOCKER_USERNAME: "${{ env.$DOCKER_LOGIN }}"
    DOCKER_PASSWORD: "${{ secrets.DOCKER_PASSWORD }}"
    IMAGE: my-image
    DESCRIPTION_PATH: "/user/local/MYREADME.md"
```


### Unsupported Options

- None
