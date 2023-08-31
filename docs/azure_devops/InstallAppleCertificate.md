# Install Apple Certificate Task

## Azure DevOps Input

```yaml
- task: InstallAppleCertificate@2
  inputs:
    certSecureFile: 'Apple_Certificates.p12'
    certPwd: '$(CERT_PWD)'
    keychain: 'temp'
```

## Transformed Github Action

```yaml
# If using a self-hosted runner, ensure the runner’s keychain is cleaned up at the end of the build
- name: Install Apple Certificate
  env:
    BUILD_CERTIFICATE_BASE64: "${{ secrets.BUILD_CERTIFICATE_BASE64 }}"
    P12_PASSWORD: "${{ secrets.P12_PASSWORD }}"
    KEYCHAIN_PASSWORD: "${{ secrets.KEYCHAIN_PASSWORD }}"
    KEYCHAIN_PATH: "${{ runner.temp }}/app-signing.keychain-db"
  run: |-
    CERTIFICATE_PATH=$RUNNER_TEMP/build_certificate.p12
    # import certificate
    echo -n "$BUILD_CERTIFICATE_BASE64" | base64 --decode --output $CERTIFICATE_PATH
    # create temporary keychain
    security create-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    security set-keychain-settings -lut 21600 $KEYCHAIN_PATH
    # import certificate to keychain
    security unlock-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    security import $CERTIFICATE_PATH -P "$P12_PASSWORD" -A -t cert -f pkcs12 -k $KEYCHAIN_PATH
    security list-keychain -d user -s $KEYCHAIN_PATH
```

## Unsupported Inputs and Aliases
None

## Addition Notes
See [here](https://docs.github.com/en/actions/guides/installing-an-apple-certificate-on-macos-runners-for-xcode-development) for details creating a base64 certificate secret
