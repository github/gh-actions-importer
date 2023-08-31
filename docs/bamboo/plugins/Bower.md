# Bower

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.bamboo-nodejs-plugin:task.builder.bower
    configuration:
      environmentVariables: BAR=bar BAZ=baz
      runtime: test-node
      command: install
      workingSubDirectory: tmp
      bowerRuntime: node_modules/bower/bin/bower
    conditions:
      - variable:
          exists: ABC
    description: Sample Bower task
```

## Transformed Github Action

```yaml
# The node-version input is optional. If not supplied, the node version from PATH will be used.
# However, it is recommended to always specify the Node.js version and not rely on the system one.
- uses: actions/setup-node@v3.6.0
  with:
    node-version: UPDATE_ME
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
- name: Sample Bower task
  run: |-
    npm install -g bower
    bower install
  working-directory: tmp
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
```

## Unsupported Options

- none
