# JsLint Builder

## Designer Pipeline

### Jenkins Input

```xml
<com.boxuk.jenkins.jslint.JSLintBuilder plugin="jslint@0.8.2">
  <includePattern>**/*.js</includePattern>
  <excludePattern>library.js</excludePattern>
  <logfile/>
  <arguments> -Dpredef=foo,bar,baz</arguments>
</com.boxuk.jenkins.jslint.JSLintBuilder>
```

### Transformed Github Action

```yaml
- name: Install jslint
  run: npm install -g jslint
- name: Run jslint
  run: jslint **/*.js -Dpredef=foo,bar,baz
```

### Unsupported Options

- Exclude pattern
- Log file

## Jenkinsfile Pipeline

- This plugin is not supported in Jenkinsfile Pipelines.
