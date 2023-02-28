# SCM Trigger

## Designer pipeline

### Jenkins input

```xml
<hudson.triggers.SCMTrigger>
  <spec>H/15 * * * *</spec>
  <ignorePostCommitHooks>false</ignorePostCommitHooks>
</hudson.triggers.SCMTrigger>
```

### Transformed Github Action

```yaml
on:
  schedule:
  - cron: "*/15 * * * *"
```

### Unsupported Options

- None

## Jenkinsfile pipeline

### Jenkins input

```groovy
triggers { pollSCM 'H */4 * * 1-5' }
```

### Transformed Github Action

```yaml
on:
  schedule:
  - cron: 4 */4 * * 1-5
```

### Unsupported Options

- TimeZone
