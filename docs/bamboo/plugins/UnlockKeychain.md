# Unlock Keychain

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.xcode.bamboo-xcode-plugin:unlockkeychain
    configuration:
      password: foo
      keychain: login
      setAsDefaultKeychain: "true"
    conditions:
      - variable:
          exists: FOO
    description: Sample unlock keychain task
```

## Transformed Github Action

```yaml
runs-on: macos-latest
steps:
  - name: Unlock Keychain
    run: security unlock-keychain -p ${{ secrets.KEYCHAIN_PASSWORD }} $KEYCHAIN_PATH
    env:
      KEYCHAIN_PATH: login.keychain
    if: env.FOO != ''
  - name: Set default keychain
    run: security default-keychain -s login.keychain
    if: env.FOO != ''
```

## Unsupported Options

- `description`
- `Change password?`
  - This option is **only** avaiable via the UI. We are unable to extract this information programmatically.
