# Ant

## Bamboo input

```yaml
Default Job:
  key: JOB1
  tasks:
  - any-task:
      plugin-key: com.atlassian.bamboo.plugins.ant:task.builder.ant
      configuration:
        environmentVariables: JAVA_OPTS="-Xmx256m -Xms128m"
        testResultsDirectory: '**/test-reports/*.xml'
        buildJdk: JDK 11
        label: test-ant-exe
        testChecked: 'true'
        target: clean test
        workingSubDirectory: test
      description: Test Task
```

## Transformed Github Action

```yaml
Default-Stage-Default-Job:
  runs-on: ubuntu-latest
  steps:
  - name: Setup Java 11
    uses: actions/setup-java@v3.11.0
    with:
      distribution: zulu
      java-version: '11'
    env:
      JAVA_OPTS: "-Xmx256m -Xms128m"
  - name: Test Task
    run: ant clean test
    env:
      JAVA_OPTS: "-Xmx256m -Xms128m"
    working-directory: test
```

## Unsupported Options

- None 
