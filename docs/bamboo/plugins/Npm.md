# Npm

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.bamboo-nodejs-plugin:task.builder.npm
    configuration:
      environmentVariables: BAR="bar" BAZ="baz"
      isolatedCache: "true"
      runtime: node
      command: version
      workingSubDirectory: src
    conditions:
      - variable:
          exists: FOO
    description: This is a sample npm task
```

## Transformed Github Action

```yaml
# The node-version input is optional. If not supplied, the node version from PATH will be used.
# However, it is recommended to always specify the Node.js version and not rely on the system one.
- uses: actions/setup-node@v3.6.0
  with:
    node-version: UPDATE_ME
    cache: npm
  if: env.FOO != ''
  env:
    BAR: bar
    BAZ: baz
- name: This is a sample npm task
  run: npm version
  working-directory: src
  if: env.FOO != ''
  env:
    BAR: bar
    BAZ: baz
```

## Unsupported Options

- none
