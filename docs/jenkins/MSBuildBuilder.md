# MsBuild Builder

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.msbuild.MsBuildBuilder plugin="msbuild@1.29">
  <msBuildName>(Default)</msBuildName>
  <msBuildFile>AwesomeSauce.csproj</msBuildFile>
  <cmdLineArgs>p:Configuration=PRODUCTION</cmdLineArgs>
  <buildVariablesAsProperties>false</buildVariablesAsProperties>
  <continueOnBuildFailure>false</continueOnBuildFailure>
  <unstableIfWarnings>false</unstableIfWarnings>
  <doNotUseChcpCommand>false</doNotUseChcpCommand>
</hudson.plugins.msbuild.MsBuildBuilder>
```

### Transformed Github Action

```yaml
- name: Install msbuild
  uses: microsoft/setup-msbuild
- name: Run msbuild
  shell: cmd
  run: msbuild AwesomeSauce.csproj p:Configuration=PRODUCTION
```

### Unsupported Options

- Pass build variables as properties
- If warnings set the build to Unstable
- Do not use chcp command

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
