# CircleCI/Docker Install Docker Credential Helper

## CircleCI Input

```yaml
orbs:
  docker: circleci/docker@x.y
jobs:
  docker-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - docker/install-docker-credential-helper:
          helper-name: pass
```

### Transformed Github Action

```yaml
- id: release_tag
  run: |-
    RELEASE_VERSION=$(curl -Ls --fail --retry 3 -o /dev/null -w %{url_effective} 'https://github.com/docker/docker-credential-helpers/releases/latest' | sed 's:.*/::')
    echo "release_tag=$RELEASE_VERSION"  >> $GITHUB_OUTPUT
- run: |-
    HELPER_FILENAME="docker-credential-${{ env.HELPER_NAME }}"
    if which "$HELPER_FILENAME" > /dev/null 2>&1; then
      echo "$HELPER_FILENAME is already installed"
      exit 0
    fi
    GPG_TEMPLATE=$(mktemp gpg_template.XXXXXX)
    cat > $GPG_TEMPLATE <<-EOF
      Key-Type: RSA
      Key-Length: 2048
      Name-Real: GitHubActions
      Expire-Date: 0
      %no-protection
      %no-ask-passphrase
      %commit
    EOF
    if [ "$HELPER_FILENAME" = "docker-credential-pass" ]; then
      sudo apt-get update --yes && sudo apt-get install gnupg2 pass --yes
      gpg2 --batch --gen-key "$GPG_TEMPLATE"
      FINGERPRINT_STRING=$(gpg2 --list-keys --with-fingerprint --with-colons GitHubActions | grep fpr)
      arrFINGERPRINT=(${FINGERPRINT_STRING//:/ })
      FINGERPRINT=${arrFINGERPRINT[-1]}
      pass init $FINGERPRINT
    fi
    rm $GPG_TEMPLATE
    curl -L -o "${HELPER_FILENAME}_archive" "https://github.com/docker/docker-credential-helpers/releases/download/${{ env.RELEASE_TAG }}/${HELPER_FILENAME}-${{ env.RELEASE_TAG }}-amd64.tar.gz"
    tar xvf "./${HELPER_FILENAME}_archive"
    chmod +x "./$HELPER_FILENAME"
    mv "./$HELPER_FILENAME" "${{ env.BIN_PATH }}/$HELPER_FILENAME"
    "${{ env.BIN_PATH }}/$HELPER_FILENAME" version
    rm "./${HELPER_FILENAME}_archive"
  env:
    HELPER_NAME: pass
    RELEASE_TAG: "${{ steps.release_tag.outputs.release_tag }}"
    BIN_PATH: "/usr/local/bin"
```

### Unsupported Options

- None
