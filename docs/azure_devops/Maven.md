# Maven Task

## Azure DevOps Input

```yaml
steps:
- task: Maven@3
  inputs:
    mavenPomFile: 'pom.xml'
    mavenOptions: '-Xmx3072m'
    javaHomeOption: 'JDKVersion'
    jdkVersionOption: '1.8'
    jdkArchitectureOption: 'x64'
    publishJUnitResults: true
    testResultsFiles: '**/surefire-reports/TEST-*.xml'
    goals: 'clean'
```

```yaml
# Maven
- task: Maven@3
  inputs:
    mavenPomFile: 'pom.xml' 
    goals: 'package' # Optional
    options: # Optional
    publishJUnitResults: true 
    testResultsFiles: '**/surefire-reports/TEST-*.xml' # Required when publishJUnitResults == True
    #testRunTitle: # Optional
    #codeCoverageToolOption: 'None' # Optional. Options: none, cobertura, jaCoCo. Enabling code coverage inserts the `clean` goal into the Maven goals list when Maven runs.
    #codeCoverageClassFilter: # Optional. Comma-separated list of filters to include or exclude classes from collecting code coverage. For example: +:com.*,+:org.*,-:my.app*.*
    #codeCoverageClassFilesDirectories: # Optional
    #codeCoverageSourceDirectories: # Optional
    #codeCoverageFailIfEmpty: false # Optional
    #javaHomeOption: 'JDKVersion' # Options: jDKVersion, path
    jdkVersionOption: 'default' # Optional. Options: default, 1.11, 1.10, 1.9, 1.8, 1.7, 1.6
    #jdkDirectory: # Required when javaHomeOption == Path
    jdkArchitectureOption: 'x64' # Optional. Options: x86, x64
    #mavenVersionOption: 'Default' # Options: default, path
    #mavenDirectory: # Required when mavenVersionOption == Path
    #mavenSetM2Home: false # Required when mavenVersionOption == Path
    #mavenOptions: '-Xmx1024m' # Optional
    #mavenAuthenticateFeed: false 
    #effectivePomSkip: false 
    #sonarQubeRunAnalysis: false 
    #sqMavenPluginVersionChoice: 'latest' # Required when sonarQubeRunAnalysis == True# Options: latest, pom
    #checkStyleRunAnalysis: false # Optional
    #pmdRunAnalysis: false # Optional
    #findBugsRunAnalysis: false # Optional

```

### Transformed Github Action

```yaml
name: Set up JDK
uses: actions/setup-java@v3.10.0
with:
  java_version: '1.8'
  architecture: x86
name: Run maven
  run: mvn --file pom.xml clean
  env:
    MAVEN_OPTS: "-Xmx3072m"
name: Publish Test Report
uses: scacap/action-surefire-report@v1
with:
  path: "**/surefire-reports/TEST-*.xml"
```

### Unsupported Inputs

- testRunTitle
- codeCoverageToolOption
- codeCoverageClassFilter
- codeCoverageClassFilesDirectories
- codeCoverageSourceDirectories
- codeCoverageFailIfEmpty
- javaHomeOption
- jdkDirectory
- mavenVersionOption
- mavenDirectory
- mavenSetM2Home
- mavenAuthenticateFeed
- effectivePomSkip
- sonarQubeRunAnalysis
- sqMavenPluginVersionChoice
- checkStyleRunAnalysis
- pmdRunAnalysis
- findBugsRunAnalysis
