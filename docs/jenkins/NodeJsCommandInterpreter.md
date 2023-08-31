# NodeJsCommandInterpreter

## Designer Pipeline

### Jenkins Input

```xml
<project>
<actions/>
<description/>
<keepDependencies>false</keepDependencies>
<properties>
<hudson.plugins.jira.JiraProjectProperty plugin="jira@3.1.1"/>
<com.dabsquared.gitlabjenkins.connection.GitLabConnectionProperty plugin="gitlab-plugin@1.5.13">
<gitLabConnection/>
</com.dabsquared.gitlabjenkins.connection.GitLabConnectionProperty>
</properties>
<scm class="hudson.scm.NullSCM"/>
<canRoam>true</canRoam>
<disabled>false</disabled>
<blockBuildWhenDownstreamBuilding>false</blockBuildWhenDownstreamBuilding>
<blockBuildWhenUpstreamBuilding>false</blockBuildWhenUpstreamBuilding>
<triggers/>
<concurrentBuild>false</concurrentBuild>
<builders>
<jenkins.plugins.nodejs.NodeJSCommandInterpreter plugin="nodejs@1.3.9">
<command>function hello(){ console.log("Hello") } hello() </command>
<configuredLocalRules/>
<nodeJSInstallationName>node</nodeJSInstallationName>
<cacheLocationStrategy class="jenkins.plugins.nodejs.cache.DefaultCacheLocationLocator"/>
</jenkins.plugins.nodejs.NodeJSCommandInterpreter>
</builders>
<publishers/>
<buildWrappers>
<jenkins.plugins.nodejs.NodeJSBuildWrapper plugin="nodejs@1.3.9">
<nodeJSInstallationName>node</nodeJSInstallationName>
<cacheLocationStrategy class="jenkins.plugins.nodejs.cache.DefaultCacheLocationLocator"/>
</jenkins.plugins.nodejs.NodeJSBuildWrapper>
</buildWrappers>
</project>
```

### Transformed Github Action

```yaml
    uses: actions/setup-node@v2
    - name: Run Node Command
      shell: node {0}
      run: |
        function hello(){
            console.log("Hello")
        }
        hello()
```

### Unsupported Options

- None

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
