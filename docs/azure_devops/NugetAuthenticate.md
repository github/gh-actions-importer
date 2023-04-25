# Nuget Authenticate Task

## Azure DevOps Input

```yaml
- task: NuGetAuthenticate@0
  displayName: 'NuGet Authenticate'
  inputs:
    nuGetServiceConnections: <connection_name> # Optional
```

## Transformed Github Action

```yaml
- name: NuGet Authenticate
  uses: actions/setup-dotnet@v3.0.3
  env:
    NUGET_AUTH_TOKEN: "${{ secrets.NUGET_AUTH_TOKEN }}"
    NUGET_FEED_URL: "${{ env.NUGET_FEED_URL }}"
  with:
    source-url: "${{ env.NUGET_FEED_URL }}"
```

## Unsupported Inputs and Aliases

- forceReinstallCredentialProvider
