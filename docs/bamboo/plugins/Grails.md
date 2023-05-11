# Grails

## Bamboo input

```yaml
- any-task:
  plugin-key: com.atlassian.bamboo.plugins.bamboo-grails-plugin:grailsBuilderTaskType
  configuration:
    testDirectoryOption: customTestDirectory
    environmentVariables: BAR=bar BAZ=baz
    testResultsDirectory: "**/*reports/*.xml,**/foo/*.xml"
    buildJdk: JDK 11
    label: grails-fake
    testChecked: "true"
    workingSubDirectory: tmp
    goals: |-
      clean
      test-app
  conditions:
    - variable:
        exists: ABC
  description: Sample Grails task
```

## Transformed Github Action

```yaml
- name: Setup Java 11
  uses: actions/setup-java@v3.11.0
  with:
    distribution: zulu
    java-version: "11"
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
- name: Sample Grails task
  working-directory: tmp
  run: |-
    grails clean --non-interactive
    grails test-app --non-interactive
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
- name: Publish test results
  uses: EnricoMi/publish-unit-test-result-action@v2.6.0
  if: env.ABC != '' && always()
  with:
    junit_files: |-
      **/*reports/*.xml
      **/foo/*.xml
  env:
    BAR: bar
    BAZ: baz
```

## Unsupported Options

none
