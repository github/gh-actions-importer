# JDK

## Travis Input

```yaml
jdk: 8
```

## Transformed Github Action

```yaml
- uses: actions/setup-java@v3.10.0
  with:
    distribution: zulu
    java-version: 8
```
