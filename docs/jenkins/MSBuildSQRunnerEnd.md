# MSBuild SQ Runner End

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.sonar.MsBuildSQRunnerEnd plugin="sonar@2.12"/>
```

### Transformed Github Action

```yaml
jobs:
  build:
    runs-on: windows-latest
    steps:
    - name: checkout
      uses: actions/checkout@v2
      with:
        fetch-depth: 0
    - name: Install SonarCloud scanner
      if: steps.cache-sonar-scanner.outputs.cache-hit != 'true'
      shell: pwsh
      run: |-
        New-Item -Path ./.sonar/scanner -ItemType Directory
        dotnet tool update dotnet-sonarscanner --tool-path ./.sonar/scanner
    - name: Begin SonarCloud Scan
      shell: pwsh
      env:
        SONAR_TOKEN: "${{ secrets.SONAR_SONARQUBE_DEV_TEST_COM_TOKEN }}"
        GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
      run: |-
        .\.sonar\scanner\dotnet-sonarscanner begin /k:"test_key"/
        /n:'Important Service   STAGING'/
        /v:'2.15'/
        /d:sonar.login=${{ env.SONAR_TOKEN }}/
        /d:sonar.verbose="true"/
        /d:sonar.cs.opencover.reportsPaths='"/path/to/coverage.xml","/path/to/coverage.2.xml"'/
        /d:sonar.coverage.exclusions='"**/*.cs","**/*.md"'
    - name: End SonarCloud Scan
      shell: pwsh
      env:
        SONAR_TOKEN: "${{ secrets.SONAR_SONARQUBE_DEV_TEST_COM_TOKEN }}"
      run: "./.sonar/scanner/dotnet-sonarscanner end /d:sonar.login=${{ env.SONAR_TOKEN }}"
```

### Unsupported Options

- None

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
