# Grunt

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.bamboo-nodejs-plugin:task.builder.grunt
    configuration:
      task: css js
      gruntRuntime: node_modules/grunt-cli/bin/grunt
      environmentVariables: BAR=bar BAZ=baz
      configFile: scratch/Gruntfile.js
      runtime: test-node
      workingSubDirectory: tmp
    conditions:
      - variable:
          exists: ABC
    description: Sample Grunt task
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
- name: Sample Grunt task
  run: |-
    npm install --save-dep grunt
    grunt css js --gruntfile scratch/Gruntfile.js
  working-directory: tmp
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
```

## Unsupported Options

- none
