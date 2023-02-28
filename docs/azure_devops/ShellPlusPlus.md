# Shell++

## Azure DevOps input

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

### Unsupported inputs

- failOnStderr
