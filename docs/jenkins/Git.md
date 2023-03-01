# Git Builder

## Designer pipeline

This plugin is not mapped to a GitHub Actions equivalent for a Designer pipeline.

## Jenkinsfile pipeline

### Jenkins input

```groovy
steps {
  git url:'https://github.com/jenkinsci/git-plugin'
}
```

### Transformed Github Action

```yaml
name: checkout
uses: actions/checkout@v2
```

### Unsupported Options

* branch
* changelog
* credentialsId
* poll
