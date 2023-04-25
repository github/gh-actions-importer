# CircleCI/Docker Install Dockerize

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/install-dockerize:
          version: latest
```

### Transformed Github Action (Composite Action Enabled)
workflow
```yaml
- uses: "./.github/actions/docker_install_dockerize"
  with:
    version: latest
```
action.yml
```yaml
name: docker_install_dockerize
inputs:
  install-dir:
    required: false
    default: "/usr/local/bin"
  version:
    required: false
    default: latest
runs:
  using: composite
  steps:
  - id: version
    run: |-
      DOCKERIZE_VERSION=$(curl --fail --retry 3 -Ls -o /dev/null -w %{url_effective} 'https://github.com/jwilder/dockerize/releases/latest' | sed 's:.*/::')
      echo "version=$DOCKERIZE_VERSION" >> $GITHUB_OUTPUT
    shell: bash
  - run: |-
      if [[ $VERSION == "latest" ]]; then VERSION="${{ steps.version.outputs.version }}"; fi
      curl -O --silent --show-error --location --fail --retry 3 "https://github.com/jwilder/dockerize/releases/download/$VERSION/dockerize-linux-amd64-$VERSION.tar.gz"
      tar xf "dockerize-linux-amd64-$VERSION.tar.gz"
      rm -f "dockerize-linux-amd64-$VERSION.tar.gz"
      mv dockerize ${{ env.INSTALL_DIR }}
      chmod +x ${{ env.INSTALL_DIR }}/dockerize
      echo "dockerize $(dockerize --version) has been installed to $(which dockerize)"
    env:
      VERSION: "${{ inputs.version }}"
      INSTALL_DIR: "${{ inputs.install-dir }}"
    shell: bash
```
### Transformed Github Action (Composite Action Disabled)
```yaml
steps:
- id: version
  run: |-
    DOCKERIZE_VERSION=$(curl --fail --retry 3 -Ls -o /dev/null -w %{url_effective} 'https://github.com/jwilder/dockerize/releases/latest' | sed 's:.*/::')
    echo "version=$DOCKERIZE_VERSION" >> $GITHUB_OUTPUT
- run: |-
    if [[ $VERSION == "latest" ]]; then VERSION="${{ steps.version.outputs.version }}"; fi
    curl -O --silent --show-error --location --fail --retry 3 "https://github.com/jwilder/dockerize/releases/download/$VERSION/dockerize-linux-amd64-$VERSION.tar.gz"
    tar xf "dockerize-linux-amd64-$VERSION.tar.gz"
    rm -f "dockerize-linux-amd64-$VERSION.tar.gz"
    mv dockerize ${{ env.INSTALL_DIR }}
    chmod +x ${{ env.INSTALL_DIR }}/dockerize
    echo "dockerize $(dockerize --version) has been installed to $(which dockerize)"
  env:
    VERSION: latest
    INSTALL_DIR: "/usr/local/bin"
```
### Unsupported Options

- None
