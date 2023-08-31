# Gulp

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.bamboo-nodejs-plugin:task.builder.gulp
    configuration:
      task: clean build
      environmentVariables: BAR=bar BAZ=baz
      gulpRuntime: node_modules/gulp/bin/gulp.js
      configFile: scratch/gulp.js
      runtime: test-node
      workingSubDirectory: tmp
    conditions:
      - variable:
          exists: ABC
    description: Sample Gulp task
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
- name: Sample Gulp task
  run: |-
    npm install --save-dev gulp
    gulp clean build --gulpfile scratch/gulp.js
  working-directory: tmp
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
```

## Unsupported Options

- none
