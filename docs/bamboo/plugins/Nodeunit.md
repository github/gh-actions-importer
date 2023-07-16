# Node

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.bamboo-nodejs-plugin:task.builder.nodeunit
    configuration:
      testResultsDir: test-reports/
      testFiles: test/
      runtime: node
      parseTestResults: 'true'
      nodeunitRuntime: node_modules/nodeunit/bin/nodeunit
```

## Transformed Github Action

```yaml
# The node-version input is optional. If not supplied, the node version from PATH will be used.
# However, it is recommended to always specify the Node.js version and not rely on the system one.
- uses: actions/setup-node@v3.6.0
  with:
    node-version: UPDATE_ME
- run: npm install nodeunit -g
  name: Install nodeunit
- run: nodeunit --reporter junit --output test-reports/ test/
  name: Absolute unit
- name: Publish nodeunit test results
  uses: EnricoMi/publish-unit-test-result-action@v2.7.0
  if: always()
  with:
    files: test-reports/
```

## Unsupported Options
- runtime
- nodeunitRuntime
