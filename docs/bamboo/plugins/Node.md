# Node

## Bamboo input

```yaml
Default Job:
  key: JOB1
  tasks:
    - any-task:
        plugin-key: com.atlassian.bamboo.plugins.bamboo-nodejs-plugin:task.builder.node
        configuration:
          environmentVariables: HI=BYE
          runtime: node
          arguments: --foo bar
          command: app.js
          workingSubDirectory: src
        conditions:
          - variable:
              exists: ABC
```

## Transformed Github Action

```yaml
- run: node --foo bar app.js
  env:
    HI: BYE
  working-directory: src
  if: env.ABC != ''
```

## Unsupported Options
- None
