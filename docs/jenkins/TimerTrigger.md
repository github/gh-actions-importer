# Timer Trigger

## Designer Pipeline

### Jenkins Input

```xml
<triggers>
   <hudson.triggers.TimerTrigger>
      <spec>H 13 * * *</spec>
   </hudson.triggers.TimerTrigger>
</triggers>
```

### Transformed Github Action

```yaml
on:
  schedule:
  - cron: 3 13 * * *
```

### Unsupported Options

- TimeZone

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
triggers {
    cron('H */4 * * 1-5')
}
```

### Transformed Github Action

```yaml
on:
  schedule:
  - cron: 4 */4 * * 1-5
```

### Unsupported Options

- TimeZone
