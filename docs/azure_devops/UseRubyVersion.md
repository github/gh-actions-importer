# UseRubyVersion task

## Azure DevOps input

```yaml
# Use Ruby version
# Use the specified version of Ruby from the tool cache, optionally adding it to the PATH
- task: UseRubyVersion@0
  inputs:
    #versionSpec: '3.7' 
    #addToPath: true # Optional
```

### Transformed Github Action

```yaml
  uses: ruby/setup-ruby@v1.138.0
  with:
    ruby-version: '3.7'
```

### Unsupported inputs

- addToPath (it is always added to PATH)

> Version ranges in `versionSpec` are not supported (eg: `>= 2.2`)