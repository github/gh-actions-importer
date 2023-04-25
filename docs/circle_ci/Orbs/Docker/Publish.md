# CircleCI/Docker Publish

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y.z
  
workflows:
  build-docker-image-only:
    jobs:
       - docker/publish:
          attach-at: path-to-attach
          cache_from: image1, image2
          extra_build_args: --compress
          path: wd
          image: my-image
          lint-dockerfile: true
          treat-warnings-as-errors: true
          step-name: My docker build step
          tag: my-tag
          registry: my-registry
          docker-password: DOCKER_PASSWORD
          docker-username: DOCKER_LOGIN
          registry: docker.io
          use-docker-credentials-store: true
          after_checkout:
            - run: echo after checkout
          before_build:
            - run: echo before build
          after_build:
            - run: echo before build
          update-description: true
```

### Transformed Github Action

```yaml
- uses: actions/checkout@v2
- run: echo after checkout
- uses: actions/download-artifact@v2
  with:
    path: path-to-attach
- id: release_tag
  run: |-
    RELEASE_VERSION=$(curl -Ls --fail --retry 3 -o /dev/null -w %<url_effective>s 'https://github.com/docker/docker-credential-helpers/releases/latest' | sed 's:.*/::')
    echo "release_tag=$RELEASE_VERSION" >> $GITHUB_OUTPUT
- run: |-
    curl -L -o '${{ env.HELPER_FILENAME }}_archive' 'https://github.com/docker/docker-credential-helpers/releases/download/${{ env.RELEASE_TAG }}/${{ env.HELPER_FILENAME }}-${{ env.RELEASE_TAG }}-amd64.tar.gz'
    tar xvf './${{ env.HELPER_FILENAME }}_archive'
    chmod +x './${{ env.HELPER_FILENAME }}'
    mv './${{ env.HELPER_FILENAME }}' '${{ env.BIN_PATH }}/${{ env.HELPER_FILENAME }}'
    '${{ env.BIN_PATH }}/${{ env.HELPER_FILENAME }}' version
    rm './${{ env.HELPER_FILENAME }}_archive'
  env:
    HELPER_FILENAME: docker-credential-pass
    RELEASE_TAG: "${{ steps.release_tag.outputs.release_tag }}"
    BIN_PATH: "/usr/local/bin"
- run: |-
    mkdir -p $(dirname $HOME/.docker/config.json)
    cat $HOME/.docker/config.json | jq --arg credsStore '${{ env.HELPER_NAME }}' '. + {credsStore: $credsStore}' > /tmp/docker-config-credsstore-update.json
    cat /tmp/docker-config-credsstore-update.json > $HOME/.docker/config.json
    rm /tmp/docker-config-credsstore-update.json
  env:
    HELPER_NAME: docker-credential-pass
- uses: docker/login-action@v2.1.0
  with:
    username: "${{ env.DOCKER_LOGIN }}"
    password: "${{ secrets.DOCKER_PASSWORD }}"
    registry: docker.io
- run: echo before build
- run: npm install -g dockerlint
- run: dockerlint -p Dockerfile
- uses: actions/download-artifact@v2
  with:
    path: path-to-attach
- run: docker pull image1
- run: docker pull image2
- name: My docker build step
  run: |-
    docker build \
    --compress \
    --cache-from image1 \
    --cache-from image2 \
    -f wd/Dockerfile \
    -t docker.io/my-image:my-tag
  timeout-minutes: 10
  continue-on-error: true
- run: echo before build
- name: My docker build step
  run: docker push docker.io/my-image:my-tag
- uses: "./.github/actions/docker_update_description"
  with:
    docker-password: "${{ secrets.DOCKER_PASSWORD }}"
    docker-username: "${{ env.DOCKER_USERNAME }}"
    image: my-image
    description-path: wd/README.md
```

### Unsupported Options

- executor
- remote-docker-dlc
- use-remote-docker
