# Replace Tokens

## Azure DevOps Input

```yaml
steps:
- task: qetza.replacetokens.replacetokens-task.replacetokens@3
  displayName: 'Replace tokens'
  inputs:
    targetFiles: |
      **/*.config
      **/*.json
```yaml

### Transformed Github Action

```yaml
- uses: cschleiden/replace-tokens@v1
  with:
    tokenPrefix: '#{'
    tokenSuffix: '}#'
    files: '["**/*.config","**/*.json"]'
```

### Unsupported Inputs

- rootDirectory
- encoding
- writeBOM
- escapeType
- escapeChar
- charsToEscape
- verbosity
- actionOnMissing
- keepToken
- useLegacyPattern
- emptyValue
- defaultValue
- enableTransforms
- transformPrefix
- transformSuffix
- variableFiles
- variableSeparator
- enableTelemetry

### Unsupported outputss

- tokenReplacedCount
- tokenFoundCount
- fileProcessedCount
- transformExecutedCount
- defaultValueCount
