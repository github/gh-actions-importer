# Team Push Trigger (Azure DevOps)

## Designer Pipeline

### Jenkins Input

```xml
<scm class="hudson.plugins.git.GitSCM" plugin="git@4.4.4">
   <configVersion>2</configVersion>
   <userRemoteConfigs>
      <hudson.plugins.git.UserRemoteConfig>
         <url>https://{VSTS account}.visualstudio.com/DefaultCollection/_git/{team project}</url>
         <credentialsId>jenkout-bot</credentialsId>
      </hudson.plugins.git.UserRemoteConfig>
   </userRemoteConfigs>
   <branches>
      <hudson.plugins.git.BranchSpec>
         <name>*/main</name>
      </hudson.plugins.git.BranchSpec>
   </branches>
   <doGenerateSubmoduleConfigurations>false</doGenerateSubmoduleConfigurations>
   <submoduleCfg class="list" />
   <extensions />
</scm>

...

<triggers>
   <hudson.plugins.tfs.TeamPushTrigger plugin="tfs@5.157.1">
      <spec />
      <jobContext>Jenkins build</jobContext>
   </hudson.plugins.tfs.TeamPushTrigger>
</triggers
```

### Transformed Github Action

```yaml
on:
  push:
    branches:
    - main
```

### Unsupported Options

- jobContext

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
