# Checkout

## Designer Pipeline

### Jenkins Input

```xml
<scm class="hudson.plugins.git.GitSCM" plugin="git@4.4.4">
    <configVersion>2</configVersion>
    <userRemoteConfigs>
        <hudson.plugins.git.UserRemoteConfig>
            <url>https://github.com/github/jenkout-fake-repo</url>
            <credentialsId>jenkout-bot</credentialsId>
        </hudson.plugins.git.UserRemoteConfig>
        <hudson.plugins.git.UserRemoteConfig>
            <url>https://github.com/valet-testing-unit/sage</url>
            <credentialsId>jenkout-bot</credentialsId>
        </hudson.plugins.git.UserRemoteConfig>
    </userRemoteConfigs>
    <branches>
        <hudson.plugins.git.BranchSpec>
            <name>*/main</name>
        </hudson.plugins.git.BranchSpec>
    </branches>
    <doGenerateSubmoduleConfigurations>false</doGenerateSubmoduleConfigurations>
    <submoduleCfg class="list"/>
    <extensions/>
</scm>
```

### Transformed Github Action

```yaml
- name: checkout
  uses: actions/checkout@v2
```

### Unsupported Options

- configVersion
- doGenerateSubmoduleConfigurations
- submoduleCfg
- extensions

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
    checkout(
            changelog: true,
            poll: false,
            scm: [
                $class: 'GitSCM',
                branches: [[name: '${CID_COMMIT}']],
                browser: [
                    $class: 'GitLab',
                    repoUrl: 'http://github.com/zoo/fastsloth',
                    version: "8.8"
                ],
                doGenerateSubmoduleConfigurations: false,
                extensions: [],
                gitTool: 'git-default',
                submoduleCfg: [],
                userRemoteConfigs: [[name: 'origin', url: 'http://github.com/zoo/fastsloth.git', refspec: '+refs/heads/*:refs/remotes/origin/* +refs/pull/*:refs/pull/*']]
            ]
    );
}
```

### Transformed Github Action

```yaml
- name: checkout
  uses: actions/checkout@v2
  with:
    repository: 'zoo/fast-sloth'
```

### Unsupported Options

- changeLog
- poll
- branches (value not reported)
- doGenerateSubmoduleConfigurations (value not reported)
- extensions (value not reported)
- gitTool (value not reported)
- submoduleCfg (value not reported)
- userRemoteConfigs (only `url` is supported)
- browser (value not reported)
