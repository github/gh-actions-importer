# Doxygen Builder

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.doxygen.DoxygenBuilder plugin="doxygen@0.18">
  <doxyfilePath>"doxy-path/files/anotherfolder/one another"</doxyfilePath>
  <installationName/>
  <continueOnBuildFailure>true</continueOnBuildFailure>
  <unstableIfWarnings>false</unstableIfWarnings>
</hudson.plugins.doxygen.DoxygenBuilder>
```

### Transformed Github Action

```yaml
name: "Doxygen"
uses: mattnotmitt/doxygen-action@v1.9.5
with:
  working-directory: 'submodule/'
  doxyfile-path: 'docs/Doxygen'
  enable-latex: true # Only if enable-latex is true
continue-on-error: true
```

### Unsupported Options

- If warnings set the build to Unstable

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
