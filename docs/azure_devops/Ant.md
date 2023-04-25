# Ant Task

## Azure DevOps Input

```yaml
- task: Ant@1
  inputs:
    buildFile: 'build.xml'                 # Required, alias: antBuildFile
    options: -DmyProperty=myPropertyValue  # Optional
    targets: build-test                    # Optional
    publishJUnitResults: true              # Required
    testResultsFiles: '**/TEST-*.xml'      # Required when publishJUnitResults == True
    testRunTitle: "Test Run Title"         # Optional
    antHomeDirectory: ANT_HOME             # Optional, alias: antHomeUserInputPath
    javaHomeOption: 'JDKVersion'           # Options: jDKVersion, path, Alias: javaHomeSelection
    jdkVersionOption: 'default'            # Optional. Options: default, 1.11, 1.10, 1.9, 1.8, 1.7, 1.6, Alias: jdkVersion
    jdkUserInputDirectory:                 # Required when javaHomeOption == Path, Alias: jdkUserInputPath
    jdkArchitectureOption: 'x64'           # Optional. Options: x86, x64, Alias: jdkArchitecture
```

### Transformed Github Action

```yaml
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
- run: ant -DmyProperty=myPropertyValue -buildfile build.xml build-test
  env:
    ANT_HOME: ANT_HOME_DIR
- name: Publish test results
  uses: EnricoMi/publish-unit-test-result-action@v2.4.1
  if: always()
  with:
    comment_title: Test Run Title
    files: "**/TEST-*.xml"
```

```yaml
# publishJUnitResults: false
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
- run: ant -DmyProperty=myPropertyValue -buildfile build.xml build-test
  env:
    ANT_HOME: ANT_HOME_DIR
```

### Unsupported Inputs

- codeCoverageToolOptions,            alias: codeCoverageTool
- codeCoverageClassFilesDirectories,  alias: classFilesDirectories
- codeCoverageClassFilter,            alias: classFilter
- codeCoverageSourceDirectories,      alias: srcDirectories
- codeCoverageFailIfEmpty,            alias: failIfCoverageEmpty
