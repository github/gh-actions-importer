# Environment Injector

## Designer Pipeline

### Jenkins Input for

_EnvInjectBuilder_
```xml
<EnvInjectBuilder plugin="envinject@2.3.0">
    <info>
        <propertiesFilePath>inject.properties</propertiesFilePath>
        <propertiesContent># this is a comment MY_USER=matz MY_HOME:/home/usr/matz</propertiesContent>
    </info>
</EnvInjectBuilder>
```

_EnvInjectBuildWrapper_
```xml
<EnvInjectBuildWrapper plugin="envinject@2.3.0">
    <info>
        <propertiesFilePath>envs.properties</propertiesFilePath>
        <propertiesContent>! This is a comment MY_NAME: matz IS_GITHUB=true</propertiesContent>
        <scriptFilePath>my_script</scriptFilePath>
        <scriptContent>#!/usr/bin/python print("Doing something") print("very important")</scriptContent>
        <secureGroovyScript plugin="script-security@1.77">
            <script>println "Groovy!!"</script>
            <sandbox>false</sandbox>
        </secureGroovyScript>
        <loadFilesFromMaster>false</loadFilesFromMaster>
    </info>
</EnvInjectBuildWrapper>
```

_EnvInjectJobProperty_
```xml
<EnvInjectJobProperty plugin="envinject@2.3.0">
    <info>
        <propertiesFilePath>my_prop.properties</propertiesFilePath>
        <propertiesContent>MY_NAME:matz</propertiesContent>
        <scriptFilePath>my_script</scriptFilePath>
        <scriptContent>echo "preparing the environment"</scriptContent>
        <secureGroovyScript plugin="script-security@1.77">
            <script>println "Almost done"</script>
            <sandbox>false</sandbox>
        </secureGroovyScript>
        <loadFilesFromMaster>false</loadFilesFromMaster>
    </info>
    <on>true</on>
    <keepJenkinsSystemVariables>true</keepJenkinsSystemVariables>
    <keepBuildVariables>true</keepBuildVariables>
    <overrideBuildParameters>false</overrideBuildParameters>
</EnvInjectJobProperty>
```

### Transformed Github Action for

_EnvInjectBuilder_
```yaml
- name: create env properties file
  shell: bash
  run: |-
    cat > ${{ github.run_id }}.properties <<'EOL'
    # this is a comment
    MY_USER=matz
    MY_HOME:/home/usr/matz
    EOL
- name: inject property file envs
  uses: actions/github-script@v6.4.0
  env:
    PROPERTIES_FILES: "${{ github.run_id }}.properties,inject.properties"
  with:
    script: |-
      const fs = require("fs")
      const files = process.env.PROPERTIES_FILES.split(",")
      files.forEach(file => {
        let envs = {}
        fs.readFile(file, 'utf8', (err, data) => {
          const lines = data
            // joins multiline properties
            .replace(/\\\n( )*/g, '')
            .split('\n')
            // removes comments and empty lines
            .filter(line => line && !line.startsWith("#") && !line.startsWith("!"))
          lines.forEach(line => {
            match = line.match(/(?<key>\w+)\s*?[=:]\s*?(?<value>\S.+)/).groups
            envs[match["key"]] = match["value"]
          })
          for (let [key, value] of Object.entries(envs)) {
            if (value.startsWith("$")) {
              const env_name = value.substring(1)
              if (env_name in process.env) {
                 value = process.env[env_name]
              } else if (env_name in envs) {
                value = envs[env_name]
              }
            }
            core.exportVariable(key, value);
          }
        })
      });
- name: clean up temp files
  shell: bash
  run: rm -f ${{ github.run_id }}.properties
```

_EnvInjectBuildWrapper_
```yaml
- name: run groovy script
  shell: groovy {0}
  run: println "Groovy!!"
- name: create env properties file
  shell: bash
  run: |-
    cat > ${{ github.run_id }}.properties <<'EOL'
    ! This is a comment
    MY_NAME: matz
    IS_GITHUB=true
    EOL
- name: inject property file envs
  uses: actions/github-script@v6.4.0
  env:
    PROPERTIES_FILES: "${{ github.run_id }}.properties,envs.properties"
  with:
    script: |-
      const fs = require("fs")
      const files = process.env.PROPERTIES_FILES.split(",")
      files.forEach(file => {
        let envs = {}
        fs.readFile(file, 'utf8', (err, data) => {
          const lines = data
            // joins multiline properties
            .replace(/\\\n( )*/g, '')
            .split('\n')
            // removes comments and empty lines
            .filter(line => line && !line.startsWith("#") && !line.startsWith("!"))
          lines.forEach(line => {
            match = line.match(/(?<key>\w+)\s*?[=:]\s*?(?<value>\S.+)/).groups
            envs[match["key"]] = match["value"]
          })
          for (let [key, value] of Object.entries(envs)) {
            if (value.startsWith("$")) {
              const env_name = value.substring(1)
              if (env_name in process.env) {
                 value = process.env[env_name]
              } else if (env_name in envs) {
                value = envs[env_name]
              }
            }
            core.exportVariable(key, value);
          }
        })
      });
- name: run script
  shell: bash
  run: |-
    cat > ./${{ github.run_id }}_script <<'EOL'
    #!/usr/bin/python
    print("Doing something")
    print("very important")
    EOL
    chmod +x ./${{ github.run_id }}_script
    ./${{ github.run_id }}_script
- name: run script file
  shell: bash
  run: "./my_script"
- name: clean up temp files
  shell: bash
  run: |-
    rm -f ${{ github.run_id }}.properties
    rm -f ./${{ github.run_id }}_script
```

_EnvInjectJobProperty_
```yaml
- name: run groovy script
      shell: groovy {0}
      run: println "Almost done"
- name: create env properties file
  shell: bash
  run: |-
    cat > ${{ github.run_id }}.properties <<'EOL'
    MY_NAME:matz
    EOL
- name: inject property file envs
  uses: actions/github-script@v6.4.0
  env:
    PROPERTIES_FILES: "${{ github.run_id }}.properties,my_prop.properties"
  with:
    script: |-
      const fs = require("fs")
      const files = process.env.PROPERTIES_FILES.split(",")
      files.forEach(file => {
        let envs = {}
        fs.readFile(file, 'utf8', (err, data) => {
          const lines = data
            // joins multiline properties
            .replace(/\\\n( )*/g, '')
            .split('\n')
            // removes comments and empty lines
            .filter(line => line && !line.startsWith("#") && !line.startsWith("!"))
          lines.forEach(line => {
            match = line.match(/(?<key>\w+)\s*?[=:]\s*?(?<value>\S.+)/).groups
            envs[match["key"]] = match["value"]
          })
          for (let [key, value] of Object.entries(envs)) {
            if (value.startsWith("$")) {
              const env_name = value.substring(1)
              if (env_name in process.env) {
                 value = process.env[env_name]
              } else if (env_name in envs) {
                value = envs[env_name]
              }
            }
            core.exportVariable(key, value);
          }
        })
      });
- name: run script
  shell: bash
  run: |-
    cat > ./${{ github.run_id }}_script <<'EOL'
    echo "preparing the environment"
    EOL
    chmod +x ./${{ github.run_id }}_script
    ./${{ github.run_id }}_script
- name: run script file
  shell: bash
  run: "./my_script"
- name: clean up temp files
  shell: bash
  run: |-
    rm -f ${{ github.run_id }}.properties
    rm -f ./${{ github.run_id }}_script
```

### Unsupported Options

_EnvInjectBuilder_
- None

_EnvInjectBuildWrapper_
- Additional classpath

_EnvInjectJobProperty_
- Keep Jenkins Environment Variables
- Keep Jenkins Build Variables
- Override Build Parameters
- Additional classpath
- Load script and properties files from the master

## Jenkinsfile Pipeline
This plugin is not supported in pipelines.
