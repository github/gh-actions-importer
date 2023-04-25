# Maven

## Designer Pipeline

### Jenkins Input

```xml
<hudson.tasks.Maven>
      <targets>my_goal</targets>
      <jvmOptions>-X5020=true</jvmOptions>
      <pom>pom.xml</pom>
      <properties># this is a property
property1=value</properties>
      <usePrivateRepository>true</usePrivateRepository>
      <settings class="jenkins.mvn.DefaultSettingsProvider"/>
      <globalSettings class="jenkins.mvn.DefaultGlobalSettingsProvider"/>
      <injectBuildVariables>true</injectBuildVariables>
    </hudson.tasks.Maven>
```

### Transformed Github Action

```yaml
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
    settings-path: "${{ github.workspace }}"
- name: Run maven
  env:
    MAVEN_OPTS: "=X5020=true"
  run: mvn -f pom.xml -Dproperty1=value my_goal
```

### Unsupported Options

- Inject build variable
- Use private Maven repository
- Global Settings (only supports `Global settings file on filesystem`)
- Settings File (Only supports `Settings in filesystem`)

## Jenkinsfile Pipeline

### Jenkins Input

```groovy

steps {
// Option 1: tools section not needed
  withMaven(
      maven: 'maven-3',
      mavenLocalRepo: '.repository',
      mavenSettingsConfig: 'my-maven-settings'
      ) {

      sh "mvn -DskipTests clean site install"
      // bat "mvn -DskipTests clean site install"
      }
// Alternate Option: parameters not needed
withMaven {
    sh "mvn -DskipTests clean site install"
    // bat "mvn -DskipTests clean site install"
  }
}

```

### Transformed Github Action

```yaml
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
    settings-path: "${{ github.workspace }}"
- name: Run maven
  run: mvn clean verify
```

### Unsupported Options

- mavenSettingsConfig
- mavenLocalRepo
- tempBinDir
- JDK
- mavenOpts
- globalMavenSettingsConfig
- globalMavenSettingsFilePath
- maven
- mavenSettingsConfig
- mavenSettingsFilePath
