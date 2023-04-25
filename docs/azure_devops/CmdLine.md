# CmdLine Task

## Azure DevOps Input

```yaml
# Command line
# Run a command line script using Bash on Linux and macOS and cmd.exe on Windows
- task: CmdLine@2
  inputs:
    script: "echo Write your commands here."
    #workingDirectory: # Optional
    #failOnStderr: false # Optional
```

```yaml
steps:
- script: string  # contents of the script to run
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
- run: echo Write your commands here.
  #working-directory:
```

### Unsupported Inputs

- failOnStderr
- target
