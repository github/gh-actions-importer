# Parameterized Trigger

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.parameterizedtrigger.TriggerBuilder plugin="parameterized-trigger@2.39">
  <configs>
    <hudson.plugins.parameterizedtrigger.BlockableBuildTriggerConfig>
      <configs>
        <hudson.plugins.parameterizedtrigger.BooleanParameters>
          <configs>
            <hudson.plugins.parameterizedtrigger.BooleanParameterConfig>
              <name>arg1</name>
              <value>true</value>
            </hudson.plugins.parameterizedtrigger.BooleanParameterConfig>
            <hudson.plugins.parameterizedtrigger.BooleanParameterConfig>
              <name>arg2</name>
              <value>false</value>
            </hudson.plugins.parameterizedtrigger.BooleanParameterConfig>
          </configs>
        </hudson.plugins.parameterizedtrigger.BooleanParameters>
        <hudson.plugins.parameterizedtrigger.NodeParameters/>
        <hudson.plugins.parameterizedtrigger.CurrentBuildParameters/>
        <hudson.plugins.parameterizedtrigger.FileBuildParameters>
          <propertiesFile>some/path to/file.txt, and/another.txt</propertiesFile>
          <failTriggerOnMissing>false</failTriggerOnMissing>
          <textParamValueOnNewLine>false</textParamValueOnNewLine>
          <useMatrixChild>false</useMatrixChild>
          <onlyExactRuns>false</onlyExactRuns>
        </hudson.plugins.parameterizedtrigger.FileBuildParameters>
        <hudson.plugins.git.GitRevisionBuildParameters plugin="git@4.4.4">
          <combineQueuedCommits>false</combineQueuedCommits>
        </hudson.plugins.git.GitRevisionBuildParameters>
        <hudson.plugins.parameterizedtrigger.matrix.MatrixSubsetBuildParameters>
          <filter>label=="${TARGET}"</filter>
        </hudson.plugins.parameterizedtrigger.matrix.MatrixSubsetBuildParameters>
        <hudson.plugins.parameterizedtrigger.PredefinedBuildParameters>
          <properties>KEY1=true KEY2=hi</properties>
          <textParamValueOnNewLine>false</textParamValueOnNewLine>
        </hudson.plugins.parameterizedtrigger.PredefinedBuildParameters>
        <hudson.plugins.parameterizedtrigger.SubversionRevisionBuildParameters>
          <includeUpstreamParameters>false</includeUpstreamParameters>
        </hudson.plugins.parameterizedtrigger.SubversionRevisionBuildParameters>
      </configs>
      <projects>freestyle-hedgehog</projects>
      <condition>ALWAYS</condition>
      <triggerWithNoParameters>false</triggerWithNoParameters>
      <triggerFromChildProjects>false</triggerFromChildProjects>
      <block>
        <buildStepFailureThreshold>
          <name>FAILURE</name>
          <ordinal>2</ordinal>
          <color>RED</color>
          <completeBuild>true</completeBuild>
        </buildStepFailureThreshold>
        <unstableThreshold>
          <name>UNSTABLE</name>
          <ordinal>1</ordinal>
          <color>YELLOW</color>
          <completeBuild>true</completeBuild>
        </unstableThreshold>
        <failureThreshold>
          <name>FAILURE</name>
          <ordinal>2</ordinal>
          <color>RED</color>
          <completeBuild>true</completeBuild>
        </failureThreshold>
      </block>
      <buildAllNodesWithLabel>false</buildAllNodesWithLabel>
    </hudson.plugins.parameterizedtrigger.BlockableBuildTriggerConfig>
  </configs>
  <configFactories>
    <hudson.plugins.parameterizedtrigger.BinaryFileParameterFactory>
      <parameterName></parameterName>
      <filePattern></filePattern>
      <noFilesFoundAction>SKIP</noFilesFoundAction>
    </hudson.plugins.parameterizedtrigger.BinaryFileParameterFactory>
    <hudson.plugins.parameterizedtrigger.FileBuildParameterFactory>
      <filePattern></filePattern>
      <noFilesFoundAction>SKIP</noFilesFoundAction>
    </hudson.plugins.parameterizedtrigger.FileBuildParameterFactory>
    <hudson.plugins.parameterizedtrigger.CounterBuildParameterFactory>
      <from></from>
      <to></to>
      <step>1</step>
      <paramExpr></paramExpr>
      <validationFail>FAIL</validationFail>
    </hudson.plugins.parameterizedtrigger.CounterBuildParameterFactory>
</configFactories>
</hudson.plugins.parameterizedtrigger.TriggerBuilder>
```

### Transformed Github Action

```yaml
name: Trigger workflow
uses: octokit/request-action@v2.x
with:
  route: POST /repos/:repository/actions/workflows/:workflow_id/dispatches
  repository: "${{ github.repository }}"
  workflow_id: freestyle-hedgehog.yml
  ref: "${{ github.ref }}"
  inputs: |-
    arg1: "true"
    arg2: "false"
    KEY1: "true"
    KEY2: "hi"
env:
  GITHUB_TOKEN: "${{ secrets.WORKFLOW_TRIGGER_TOKEN }}"
```

### Unsupported Options

- Block until the triggered projects finish their builds (when `true`)

Parameters:

- Build on the same node
- Current Build Parameters
- Parameters from properties files
- Pass-through Git Commit that was built
- Restrict matrix execution to a subset
- Subversion revision

Parameter Factories

- For every matching file, invoke one build
- For every property fie, invoke one build
- Invoke i=0..N builds

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
        build job: 'freestyle-hedgehog',
        parameters: [
            booleanParam(name: 'arg1', value: true),
            string(name: 'arg2', value: "hi")
        ],
        propagate: false,
        quietPeriod: 1
}
```

### Transformed Github Action

```yaml
name: Trigger workflow
uses: octokit/request-action@v2.x
with:
  route: POST /repos/:repository/actions/workflows/:workflow_id/dispatches
  repository: "${{ github.repository }}"
  workflow_id: freestyle-hedgehog.yml
  ref: "${{ github.ref }}"
env:
  GITHUB_TOKEN: "${{ secrets.WORKFLOW_TRIGGER_TOKEN }}"
```

### Unsupported Options

- wait
- propagate
- quietPeriod
- parameters

