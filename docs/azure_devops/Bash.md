# Bash Task

## Azure DevOps Input

```yaml
steps:
- task: ShellScript@2
  inputs:
    scriptPath:
    # args: # Optional
    # disableAutoCwd: false # optional
    # cwd:  # Optional
    # failOnStderr: false # Optional
```

```yaml
# Bash
# Run a Bash script on macOS, Linux, or Windows
- task: Bash@3
  inputs:
    #targetType: 'filePath' # Optional. Options: filePath, inline
    #filePath: # Required when targetType == FilePath
    #arguments: # Optional
    #script: '# echo Hello world' # Required when targetType == inline
    #workingDirectory: # Optional
    #failOnStderr: false # Optional
    #noProfile: true # Optional
    #noRc: true # Optional
```

```yaml
steps:
- bash: string  # contents of the script to run
  displayName: string  # friendly name displayed in the UI
  name: string  # identifier for this step (A-Z, a-z, 0-9, and underscore)
  workingDirectory: string  # initial working directory for the step
  failOnStderr: boolean  # if the script writes to stderr, should that be treated as the step failing?
  condition: string
  continueOnError: boolean  # 'true' if future steps should run even if this step fails; defaults to 'false'
  enabled: boolean  # whether to run this step; defaults to 'true'
  target:
    container: string # where this step will run; values are the container name or the word 'host'
    commands: enum  # whether to process all logging commands from this step; values are `any` (default) or `restricted`
  timeoutInMinutes: number
  env: { string: string }  # list of environment variables to add
```

### Transformed Github Action

```yaml
- run: sudo npm install -g appcenter-cli@1.1.20
  shell: bash
```

### Unsupported Inputs

- failOnStderr
- target
