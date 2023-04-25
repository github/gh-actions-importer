# Colins Replace Tokens Task

## Azure DevOps Input

```yaml
- task: colinsalmcorner.colinsalmcorner-buildtasks.replace-tokens-task.ReplaceTokens@1
  inputs:
    sourcePath: 
    filePattern: 'settings.*'
    tokenRegex: '__(\w+)__'
```

## Transformed Github Action

```yaml
# Tokens will be matched using pattern '__<env_name>__', if no match is found it will be set to a empty string
- uses: cschleiden/replace-tokens@v1
  with:
    files: '["settings.*"]'
    tokenPrefix: __
    tokenSuffix: __
```

## Unsupported Inputs and Aliases
- Token Regex:  Only the prefix and suffix from the regex will be used. The group seletor will be ignored
- secretTokens
