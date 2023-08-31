# Tokenization Task

## Azure DevOps Input

```yaml
- task: Tokenization@2
  inputs:
    SourcePath: 'path/to/file'              #Required, Defaults to ""
    TargetFileNames: 'file1.json,*.config'  #Required, Defaults to ""
    #RecursiveSearch: false                 #Optional, Defaults to true
    #TokenStart: '{'                        #Optional, Defaults to __
    #TokenEnd: '}'                          #Optional, Defaults to __

```

## Transformed Github Action

```yaml
- uses: cschleiden/replace-tokens@v1
  with:
    tokenPrefix: __
    tokenSuffix: __
    files: '["path/to/file/**/file1.json", "path/to/file/**/*.config"]'
```

## Unsupported Inputs and Aliases
- RequireVariable

## Additional Information
- Tokens that are found within the specified files that do not have a corresponding environment variable will be replaced by an empty string
