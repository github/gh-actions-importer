# Post Build Script

## Designer Pipeline

### Jenkins Input

```xml
<builders>
    <hudson.tasks.Shell>
        <command>
        # Create generic script
        cat > my_script << 'EOL'
        #!/usr/bin/python
        print("** Running my script on  failure..")
        EOL
        chmod +x my_script
        # Create groovy script
        cat > my_groovy.groovy << 'EOL'
        println "**   Running my groovy script on success!"
        EOL 
        </command>
        <configuredLocalRules/>
    </hudson.tasks.Shell>
</builders>
<publishers>
<org.jenkinsci.plugins.postbuildscript.PostBuildScript plugin="postbuildscript@2.11.0">
    <config>
        <scriptFiles>
            <org.jenkinsci.plugins.postbuildscript.model.ScriptFile>
                <results>
                    <string>FAILURE</string>
                </results>
                <role>BOTH</role>
                <filePath>./my_script</filePath>
                <scriptType>GENERIC</scriptType>
                <sandboxed>false</sandboxed>
            </org.jenkinsci.plugins.postbuildscript.model.ScriptFile>
            <org.jenkinsci.plugins.postbuildscript.model.ScriptFile>
                <results>
                    <string>SUCCESS</string>
                </results>
                <role>BOTH</role>
                <filePath>my_groovy.groovy</filePath>
                <scriptType>GROOVY</scriptType>
                <sandboxed>false</sandboxed>
            </org.jenkinsci.plugins.postbuildscript.model.ScriptFile>
        </scriptFiles>
        <groovyScripts>
            <org.jenkinsci.plugins.postbuildscript.model.Script>
                <results>
                    <string>SUCCESS</string>
                </results>
                <role>BOTH</role>
                <content>println "** This is my added groovy script" println "which runs on success!"</content>
                <sandboxed>false</sandboxed>
            </org.jenkinsci.plugins.postbuildscript.model.Script>
        </groovyScripts>
        <buildSteps>
            <org.jenkinsci.plugins.postbuildscript.model.PostBuildStep>
                <results>
                    <string>SUCCESS</string>
                </results>
                <role>BOTH</role>
                <buildSteps>
                    <hudson.plugins.python.Python plugin="python@1.3">
                        <command>print("** Running python script in post build step!")</command>
                        <configuredLocalRules/>
                    </hudson.plugins.python.Python>
                    <jenkins.plugins.nodejs.NodeJSCommandInterpreter plugin="nodejs@1.3.9">
                        <command>console.log("Nooodddeee!")</command>
                        <configuredLocalRules/>
                        <nodeJSInstallationName>node</nodeJSInstallationName>
                        <cacheLocationStrategy class="jenkins.plugins.nodejs.cache.PerJobCacheLocationLocator"/>
                    </jenkins.plugins.nodejs.NodeJSCommandInterpreter>
                </buildSteps>
                <stopOnFailure>false</stopOnFailure>
            </org.jenkinsci.plugins.postbuildscript.model.PostBuildStep>
            <org.jenkinsci.plugins.postbuildscript.model.PostBuildStep>
                <results>
                    <string>ABORTED</string>
                </results>
                <role>BOTH</role>
                <buildSteps>
                    <hudson.tasks.Shell>
                        <command>echo "I am a shell build step that runs on cancel!!"</command>
                        <configuredLocalRules/>
                    </hudson.tasks.Shell>
                </buildSteps>
                <stopOnFailure>false</stopOnFailure>
            </org.jenkinsci.plugins.postbuildscript.model.PostBuildStep>
            <org.jenkinsci.plugins.postbuildscript.model.PostBuildStep>
                <results>
                    <string>NOT_BUILT</string>
                </results>
                <role>BOTH</role>
                <buildSteps>
                    <hudson.tasks.Shell>
                        <command>echo "I don't run because 'NOT _BUILT' is not supported"</command>
                        <configuredLocalRules/>
                    </hudson.tasks.Shell>
                </buildSteps>
                <stopOnFailure>false</stopOnFailure>
            </org.jenkinsci.plugins.postbuildscript.model.PostBuildStep>
        </buildSteps>
        <markBuildUnstable>false</markBuildUnstable>
    </config>
</org.jenkinsci.plugins.postbuildscript.PostBuildScript>
</publishers>
<buildWrappers/>
```

### Transformed Github Action

```yaml
    - name: run command
      shell: bash
      run: |-
        # Create generic script
        cat > my_script << 'EOL'
        #!/usr/bin/python
        print("** Running my script on failure..")
        EOL
        chmod +x my_script
        # Create groovey script
        cat > my_groovy.groovy << 'EOL'
        println "** Running my groovy script on success!"
        EOL
    - name: run script file
      shell: bash
      run: "./my_script"
      if: "${{ failure() }}"
    - name: run script file
      shell: bash
      run: groovy ./my_groovy.groovy
      if: "${{ success() }}"
    - name: run groovy script
      shell: groovy {0}
      run: |-
        println "** This is my added groovy script"
        println "which runs on success!"
      if: "${{ success() }}"
    - name: run command
      shell: python
      run: print("** Running python script in post build step!")
      if: "${{ success() }}"
    - uses: actions/setup-node@v2
      if: "${{ success() }}"
    - name: Run Node Command
      shell: node {0}
      run: console.log("Nooodddeee!")
      if: "${{ success() }}"
    - name: run command
      shell: bash
      run: echo "I am a shell build step that runs on cancel!!"
      if: "${{ cancelled() }}"
#     # org.jenkinsci.plugins.postbuildscript.model.PostBuildStep was not transformed because there is no suitable equivalent for a job status of 'NOT_BUILT' in GitHub Actions
#     - name: run command
#       shell: bash
#       run: echo "I don't run because 'NOT _BUILT' is not supported"
#       if: NOT_BUILT
```

### Unsupported Options

- Execution is limited to
- Stop processing, if any build step fails

## Jenkinsfile Pipeline
This plugin is not supported in Designer Pipelines.
