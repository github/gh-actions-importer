# NodeTool Task

## Azure DevOps Input

```yaml
# Node.js tool installer
# Finds or downloads and caches the specified version spec of Node.js and adds it to the PATH
- task: NodeTool@0
  inputs:
    #versionSpec: '10.x' 
    #force32bit: false # Optional
    #checkLatest: false # Optional
```

### Transformed Github Action

```yaml
  - uses: actions/setup-node@v2
    with:
      node-version: 10.x
      architecture: x86
```

> force32bit: true sets architecture to x86

### Unsupported Inputs

- checkLatest
