# Single Conditional Builder

## Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.conditionalbuildstep.singlestep.SingleConditionalBuilder plugin="conditional-buildstep@1.3.6">
  <condition class="org.jenkins_ci.plugins.run_condition.core.AlwaysRun" plugin="run-condition@1.3"/>
  <buildStep class="hudson.tasks.Shell">
    <command>echo "hello world"</command>
    <configuredLocalRules>
      <jenkins.tasks.filters.impl.RetainVariablesLocalRule>
        <variables>RETAIN</variables>
        <retainCharacteristicEnvVars>true</retainCharacteristicEnvVars>
        <processVariablesHandling>RESET</processVariablesHandling>
      </jenkins.tasks.filters.impl.RetainVariablesLocalRule>
    </configuredLocalRules>
    <unstableReturn>23</unstableReturn>
  </buildStep>
  <runner class="org.jenkins_ci.plugins.run_condition.BuildStepRunner$Fail" plugin="run-condition@1.3"/>
</org.jenkinsci.plugins.conditionalbuildstep.singlestep.SingleConditionalBuilder>
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

## Jenkinsile pipeline

This plugin is not supported in Jenkinsfile Pipelines.
