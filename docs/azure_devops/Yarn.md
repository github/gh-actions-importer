# Yarn Task

## Azure DevOps Input

```yaml
#Execute yarn command with optional arguments and custom Azure DevOps registries and authentication
- task: Yarn@3
  inputs: 
    projectDirectory: path/to/project                       # Optional
    arguments: build --verbose                              # Optional
    productionMode: false                                   # Optional
    customRegistry: useNpmrc                                # Optional
    #customFeed: 314a13d9-056a-4c0b-bcec-64d99aae0323       # Required when customRegistry == useFeed
    customEndpoint: 671e9496-9011-4a21-9dbf-b97f41d496eb    # Optional

```

## Transformed Github Action

```yaml
- run: |-
    npm set //pkgs.dev.azure.com/adoOrg/b675ba30-3f64-43c8-b35d-79c162dc3fd7/_packaging/NpmFeed1/npm/registry/:username=user
    npm set //pkgs.dev.azure.com/adoOrg/b675ba30-3f64-43c8-b35d-79c162dc3fd7/_packaging/NpmFeed1/npm/registry/:_password=${{ env.NPM_PASSWORD_1 }}
    npm set //pkgs.dev.azure.com/adoOrg/b675ba30-3f64-43c8-b35d-79c162dc3fd7/_packaging/NpmFeed1/npm/registry/:email=email
    npm set //pkgs.dev.azure.com/adoOrg/b675ba30-3f64-43c8-b35d-79c162dc3fd7/_packaging/NpmFeed1/npm/:username=user
    npm set //pkgs.dev.azure.com/adoOrg/b675ba30-3f64-43c8-b35d-79c162dc3fd7/_packaging/NpmFeed1/npm/:_password=${{ env.NPM_PASSWORD_1 }}
    npm set //pkgs.dev.azure.com/adoOrg/b675ba30-3f64-43c8-b35d-79c162dc3fd7/_packaging/NpmFeed1/npm/:email=email
    yarn --cwd path/to/project build --verbose 
  env:
    NPM_PASSWORD_1: "${{ secrets.REPLACE_WITH_PASSWORD }}"

```

## Unsupported Inputs and Aliases
Authentication for custom registries aside from Azure DevOps
