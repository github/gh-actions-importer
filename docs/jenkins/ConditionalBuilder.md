# Conditional Builder

## Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.conditionalbuildstep.ConditionalBuilder plugin="conditional-buildstep@1.3.6">
  <runner class="org.jenkins_ci.plugins.run_condition.BuildStepRunner$Fail" plugin="run-condition@1.3"/>
  <runCondition class="org.jenkins_ci.plugins.run_condition.core.AlwaysRun" plugin="run-condition@1.3"/>
  <runCondition class="org.jenkins_ci.plugins.run_condition.core.NumericalComparisonCondition" plugin="run-condition@1.3">
      <lhs>2</lhs>
      <rhs>3</rhs>
      <comparator class="org.jenkins_ci.plugins.run_condition.core.NumericalComparisonCondition$LessThan"/>
  </runCondition>
  <conditionalbuilders>
    <hudson.tasks.Shell>
      <command>echo "hello, world!"</command>
      <configuredLocalRules/>
    </hudson.tasks.Shell>
  </conditionalbuilders>
</org.jenkinsci.plugins.conditionalbuildstep.ConditionalBuilder>
```

### Transformed Github Action

```yaml
name: run command
shell: bash
run:  "echo "hello, world!"
if:   always()
```

### Unsupported Options

The following run conditions are not supported:

- File exists
- Files match
- Numerical comparison
- Regular expression match
- Time
- Day of week
- Build cause
- Execution node
- Legacy boolean condition (deprecated)

The following options are not supported

- On evaluation failure

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
stage('Example Deploy') {
    when {
      branch 'production'
    }
    steps {
      echo 'Deploying'
    }
}
```

### Transformed Github Action

```yaml
jobs:
  example-deploy:
    if: github.ref == 'production'
    runs-on: ubuntu-latest
    steps:
```

### Unsupported Options

- changelog
- changeset
- expression
- not
- allOf
- anyOf
- triggeredBy
- environment
