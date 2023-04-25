# Sonar Runner Builder

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.sonar.SonarRunnerBuilder plugin="sonar@2.12">
    <project>nil</project>
    <properties>"sonar.projectKey=\nsonar.projectName=happykoalas\nsonar.projectVersion=1.0\nsonar.sources=src\n"<properties/>
    <javaOpts/>nil<javaOpts/>
    <additionalArguments>"-X"<additionalArguments/>
    <jdk>(Inherit From Job)</jdk>
    <sonarScannerName>"sonar-devdept"</sonarScannerName>
    <task>test</task>
</hudson.plugins.sonar.SonarRunnerBuilder>
```

### Transformed Github Action

```yaml
name: SonarCloud Scan
uses: sonarsource/sonarcloud-github-action@v1.8
env:
  GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
  SONAR_TOKEN: ${{ secrets.SONARCLOUDTOKEN }}
with:
  projectBaseDir: ${{ github.workspace }}
  args:
```

### Unsupported Options

- JVM Options
- JDK
- SonarQube Scanner (transformed into a secret to be configured and manual task is added to the PR)
- Task to run

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
