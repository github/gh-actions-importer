# Ant

## Designer Pipeline

### Jenkins Input

```xml
<hudson.tasks.Ant plugin="ant@1.11">
  <targets>target</targets>
  <antOpts>-Xms1024m
-Xms1024m</antOpts>
  <buildFile>my-build-file</buildFile>
  <properties>properties</properties>
</hudson.tasks.Ant>
```

### Transformed Github Action

```yaml
  - name: Set up JDK 1.11
    uses: actions/setup-java@v3.10.0
    with:
      java-version: '1.11'
      settings-path: "${{ github.workspace }}"
  - name: run ant
    run: Ant -D  teamcity.build.customer=${customer} -D teamcity.build.debug=false -buildfile ${PROJECT_NAME}/build.xml clean make
    env:
      ANT_OPTS: '-Xms1024m -Xmx1024m'
```

### Unsupported Options

- Ant Version
- JDK Version

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps{
      withAnt(installation: 'ant_latest') {
        sh label: '', script: '/usr/local/sandeep/hybris/bin/platform/apache-ant/bin/ant -version'
        sh "ant -f 'config/build.xml' jenkinsFinalTest -Dtest-file-name='${libFolder}/${f.name}'"
  }
}
```

### Transformed Github Action

```yaml
    steps:
    - name: Set up JDK 1.11
      uses: actions/setup-java@v3.10.0
      with:
        java-version: '1.11'
        settings-path: "${{ github.workspace }}"
    - name: sh
      shell: bash
      run: "/usr/local/sandeep/hybris/bin/platform/apache-ant/bin/ant -version"
    - name: sh
      run: '"ant -f ''config/build.xml'' jenkinsFinalTest -Dtest-file-name=''${libFolder}/${f.name''"'
```

### Unsupported Options

- Ant Version
- JDK Version
