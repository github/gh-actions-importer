# NPM Task

## Azure DevOps Input

```yaml
# npm task
# Install and publish npm packages, or run an npm command.
- task: Npm@1
  inputs:
    command: 'install' # Options: install, publish, custom
    workingDir: # Optional
    verbose: # Optional
    customCommand: # Required when command == Custom
    customRegistry: 'useNpmrc' # Optional. Options: useNpmrc, useFeed
    customFeed: # Required when customRegistry == UseFeed
    customEndpoint: # Optional
    publishRegistry: 'useExternalRegistry' # Optional. Options: useExternalRegistry, useFeed
    publishFeed: # Required when publishRegistry == UseFeed
    publishPackageMetadata: true # Optional
    publishEndpoint: # Required when publishRegistry == UseExternalRegistry
```

### Transformed Github Action

```yaml
  - name: Run npm
    run: npm install
```

### Unsupported Inputs

- verbose
- customRegistry
- customFeed
- customEndpoint
- publishRegistry
- publishFeed
- publishPackageMetadata
- publishEndpoint
