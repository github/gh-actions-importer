# Publish Symbols Task

## Azure DevOps Input

```yaml
steps:
- task: PublishSymbols@2
  inputs:
    SearchPattern: '**/bin/**/*.pdb'
    SymbolServerType: 'TeamServices'
    SymbolsMaximumWaitTime: '12'
```

## Transformed Github Action

```yaml
- uses: microsoft/action-publish-symbols@v1
  timeout-minutes: 12
  with:
    accountName: "${{ env.AZ_ACCOUNT_NAME }}"
    personalAccessToken: "${{ secrets.PUBLISH_SYMBOLS_ACCESS_TOKEN }}"
    symbolsFolder: "${{ github.workspace }}"
    searchPattern: "**/bin/**/*.pdb"
    symbolServiceUrl: "${{ env.ARTIFACT_SERVICE_URL }}"
  env:
    AZ_ACCOUNT_NAME: UPDATE_ME
    ARTIFACT_SERVICE_URL: "'https://artifacts.dev.azure.com"
```

## Unsupported Inputs and Aliases
- symbolServerType: only `TeamServices` type supported
- indexSources
- publishSymbols: `false` is not a supported value
- symbolsPath
- compressSymbols
- detailedLog
- treatNotIndexedAsWarning
- useNetCoreClientTool
- symbolsProduct
- symbolsVersion
- symbolsArtifactName
