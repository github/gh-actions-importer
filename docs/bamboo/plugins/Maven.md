# Maven

## Bamboo input

```yaml
Maven 3 Job:
  key: JOB1
  description: Task configured to use Maven 3
  tasks:
    - checkout:
        force-clean-build: false
        description: Checkout Default Repository
    - maven:
        executable: maven3
        jdk: JDK 11
        goal: clean test
        tests: "true"
        environment: FOO=BAR
        conditions:
          - variable:
              exists: HELLO
  artifact-subscriptions: []
```

## Transformed Github Action

```yaml
- name: Setup Java 11
  uses: actions/setup-java@v3.10.0
  with:
    distribution: zulu
    java-version: "11"
- name: Run Maven
  run: mvn clean test
  env:
    FOO: BAR
  if: env.HELLO != ''
```

## Unsupported Options

- `useMavenReturnCode`
