# GitHub Push Request Trigger

## Designer Pipeline

### Jenkins Input

```xml
<scm class="hudson.plugins.git.GitSCM" plugin="git@4.4.4">
   <configVersion>2</configVersion>
   <userRemoteConfigs>
      <hudson.plugins.git.UserRemoteConfig>
         <url>https://github.com/github/jenkout-fake-repo/</url>
         <credentialsId>jenkout-bot</credentialsId>
      </hudson.plugins.git.UserRemoteConfig>
   </userRemoteConfigs>
   <branches>
      <hudson.plugins.git.BranchSpec>
         <name>*/main</name>
      </hudson.plugins.git.BranchSpec>
      <hudson.plugins.git.BranchSpec>
         <name>*/alt-branch</name>
      </hudson.plugins.git.BranchSpec>
   </branches>
   <doGenerateSubmoduleConfigurations>false</doGenerateSubmoduleConfigurations>
   <submoduleCfg class="list" />
   <extensions />
</scm>

...

<triggers>
   <com.cloudbees.jenkins.GitHubPushTrigger plugin="github@1.31.0">
      <spec />
   </com.cloudbees.jenkins.GitHubPushTrigger>
</triggers>

```

### Transformed Github Action

```yaml
on:
  push:
    branches:
    - main
    - alt-branch
```

### Unsupported Options

- Lightweight checkout
- Repositories
- Repository browser
- Additional Behaviours (all)

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
triggers {
   githubPush()
}
```

### Transformed Github Action

```yaml
on:
  push:
    branches:
      - main
```

### Unsupported Options

- Lightweight checkout
- Repositories
- Repository browser
- Additional Behaviours (all)
