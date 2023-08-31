# CmdLine Task

## Azure DevOps Input

```yaml
# Command line
# Run a Windows command or batch script and optionally allow it to change the environment
- task: BatchScript@1
  inputs:
    filename: build.bat
    #arguments: # Optional
    #modifyEnvironment: False # Optional
    #workingFolder: # Optional
    #failOnStandardError: false # Optional
```

### Transformed Github Action

```yaml
- run: build.bat
  shell: cmd
```

### Unsupported Inputs

- failOnStderr
- modifyEnvironment
