# UsePythonVersion Task

## Azure DevOps Input

```yaml
# Use Python version
# Use the specified version of Python from the tool cache, optionally adding it to the PATH
- task: UsePythonVersion@0
  inputs:
    #versionSpec: '3.x' 
    #addToPath: true 
    #architecture: 'x64' # Options: x86, x64 (this argument applies only on Windows agents)
```

### Transformed Github Action

```yaml
- name: Setup Python 3.7
  uses: actions/setup-python@v1
  with:
    python-version: '3.7'
    architecture: x64
```

### Unsupported Inputs

- addToPath (it is always added to PATH)
