# MSTest Builder

## Jenkins Input

```xml
<org.jenkinsci.plugins.MsTestBuilder plugin="mstestrunner@1.3.0">
  <msTestName>(Default)</msTestName>
  <testFiles>\testcontainer\n\\otherTestPath\n"path with a\\white space"</testFiles>
  <categories>Priority1&SpeedTest</categories>
  <resultFile>Result-File-Name-Field</resultFile>
  <cmdLineArgs>/nologo /noisolation</cmdLineArgs>
  <continueOnFail>true</continueOnFail>
</org.jenkinsci.plugins.MsTestBuilder>
```

### Transformed Github Action

```yaml
- name: Install msbuild
  uses: microsoft/setup-msbuild@v1.3.1
- name: run mstest
  shell: cmd
  continue-on-error: true #optional
  run: msbuild /resultsfile:<resultFile> /testcontainer:<testFiles> /category:<categories> <cmdLineArgs>
```

### Unsupported Options

- None

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
