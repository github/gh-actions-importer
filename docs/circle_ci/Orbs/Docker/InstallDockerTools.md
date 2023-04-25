# CircleCI/Docker Install Docker Tools

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/install-docker-tools:
          dockerize-version: v1
          dockerize-install-dir: dockerize/path
          goss-verison: v2
          goss-install-dir: goss/path
```

### Transformed Github Action (Composite Action Enabled)

```yaml
steps:
- uses: "./.github/actions/docker_install_dockerize"
  with:
    install-dir: dockerize/path
    version: v1
- run: curl -fsSL https://goss.rocks/install | GOSS_DST='goss/path' sh
```

### Transformed Github Action (Composite Action Disabled)
```yaml
steps:
- run: |-
    if [[ $VERSION == "latest" ]]; then VERSION="${{ steps.version.outputs.version }}"; fi
    curl -O --silent --show-error --location --fail --retry 3 "https://github.com/jwilder/dockerize/releases/download/$VERSION/dockerize-linux-amd64-$VERSION.tar.gz"
    tar xf "dockerize-linux-amd64-$VERSION.tar.gz"
    rm -f "dockerize-linux-amd64-$VERSION.tar.gz"
    mv dockerize ${{ env.INSTALL_DIR }}
    chmod +x ${{ env.INSTALL_DIR }}/dockerize
    echo "dockerize $(dockerize --version) has been installed to $(which dockerize)"
  env:
    VERSION: v1
    INSTALL_DIR: dockerize/path
- run: curl -fsSL https://goss.rocks/install | GOSS_DST='goss/path' sh
```
### Unsupported Options

- None
