# Gradle Task

## Azure DevOps Input

```yaml
- task: Gradle@2
  inputs:
    cwd: './test-gradle'                   # Optional, alias: workingDirectory
    gradleWrapperFile: 'gradlew'           # Required, alias: wrapperScript
    tasks: 'build'                         # Required, default: build
    options: "-DProp=One"                  # Optional
    javaHomeOption: 'JDKVersion'           # Options: JDKVersion, path, Alias: javaHomeSelection
    jdkVersionOption: 'default'            # Optional. Options: default, 1.11, 1.10, 1.9, 1.8, 1.7, 1.6, Alias: JDKVersion
    jdkUserInputDirectory:                 # Required when javaHomeOption == Path, Alias: jdkUserInputPath
    jdkArchitectureOption: 'x64'           # Optional. Options: x86, x64, Alias: jdkArchitecture
    publishJUnitResults: true              # Required
    testResultsFiles: '*/test-file*.xml'   # Required when publishJUnitResults == true
    testRunTitle: "Test Run Title"         # Optional
    gradleOptions: '-Xmx1024m'             # Optional
    sonarQubeRunAnalysis: true             # Optional
    sqGradlePluginVersionChoice: 'specify' # Required when sonarQubeRunAnalysis == True Options: specify, build
    sonarQubeGradlePluginVersion: '2.5'    # Required when sonarQubeRunAnalysis == True && SqGradlePluginVersionChoice == Specify
    checkStyleRunAnalysis: false           # Optional
    findBugsRunAnalysis: false             # Optional
    pmdRunAnalysis: false                  # Optional
```

### Transformed Github Action

```yaml
- name: Set up JDK 1.8
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.8'
- name: Run gradle
  run: "./gradlew build sonarqube -DProp=One -Dsonar.projectVersion=2.5"
  working-directory: ./test-gradle
  env:
    GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
    SONAR_TOKEN: "${{ secrets.SONAR_TOKEN }}"
    GRADLE_OPTS: -Xmx1024m

- name: Publish test results
  uses: EnricoMi/publish-unit-test-result-action@v2.4.1
  if: always()
  with:
    comment_title: Test Run Title
    files: "*/test-file*.xml"
```

### Unsupported Inputs

- codeCoverageToolOptions,            alias: codeCoverageTool
- codeCoverageClassFilesDirectories,  alias: classFilesDirectories
- codeCoverageClassFilter,            alias: classFilter
- codeCoverageSourceDirectories,      alias: srcDirectories
- codeCoverageFailIfEmpty,            alias: failIfCoverageEmpty
- checkstyleAnalysisEnabled,          alias: checkStyleRunAnalysis
- findbugsAnalysisEnabled,            alias: findBugsRunAnalysis
- pmdAnalysisEnabled,                 alias: pmdRunAnalysis
