# Yarn Installer task

## Azure DevOps input

```yaml
# Install specified version of yarn
- task: YarnInstaller@3
  inputs:
    versionSpec: 1.22.*         # Required, Defaults to 1.x
    #checkLatest: false         # Whether to download the latest matching version, Defaults to false
    #includePrerelease: false   # Whether to include prerelease versions, Defaults to false
```

## Transformed Github Action

```yaml
- run: npm install yarn@"1.22.*"
```

## Unsupported inputs and aliases

- checkLatest
- includePrerelease