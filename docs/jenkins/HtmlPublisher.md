# HTML Publisher

## Designer Pipeline

### Jenkins Input

```xml
<htmlpublisher.HtmlPublisher plugin="htmlpublisher@1.25">
  <reportTargets>
    <htmlpublisher.HtmlPublisherTarget>
      <reportName>HTML Report</reportName>
      <reportDir>my/html/path</reportDir>
      <reportFiles>index.html</reportFiles>
      <alwaysLinkToLastBuild>false</alwaysLinkToLastBuild>
      <reportTitles>My Results</reportTitles>
      <keepAll>false</keepAll>
      <allowMissing>false</allowMissing>
      <includes>**/*.html,**/*.css</includes>
      <escapeUnderscores>true</escapeUnderscores>
    </htmlpublisher.HtmlPublisherTarget>
  </reportTargets>
</htmlpublisher.HtmlPublisher>
```

### Transformed Github Action

```yaml
name: Upload Artifacts
uses: actions/upload-artifact@v2
with:
  name: HTML Report
  path: |-
    my/html/path/**/*.html
    my/html/path/**/*.css
```

### Unsupported Options

- Index Pages
- Index page titles
- Keep past HTML reports
- Always link to last build
- Escape underscores in report title

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
stage('Example Deploy') {
    steps {
        publishHTML (target: [
          allowMissing: false,
          alwaysLinkToLastBuild: false,
          keepAll: true,
          reportDir: 'my/html/path',
          reportFiles: 'index.html',
          reportName: "HTML Report",
          includes: "**/*.html"
        ])
    }
}
```

### Transformed Github Action

```yaml
jobs:
  Example-Deploy:
    name: Example Deploy
    steps:
      name: Upload Artifacts
      uses: actions/upload-artifact@v2
      with:
        name: HTML Report
        path: my/html/path/**/*.html
```

### Unsupported Options

- Index Pages
- Index page titles
- Keep past HTML reports
- Always link to last build
- Escape underscores in report title
