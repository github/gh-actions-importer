# Shell++

## Azure DevOps Input

```yaml
steps:
- task: Shellpp@0
  inputs:
    type: 'InlineScript'
    script: echo Hello World
```


### Transformed Github Action

```yaml
- run: echo Hello World
  shell: bash
```

### Unsupported Inputs

- failOnStderr
