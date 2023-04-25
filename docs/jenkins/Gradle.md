# Gradle

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.gradle.Gradle plugin="gradle@1.36">
    <switches>--build-cache --console=plain </switches>
    <tasks>clean install</tasks>
    <rootBuildScriptDir>${WORKSPACE}/something</rootBuildScriptDir>
    <buildFile>something.build</buildFile>
    <gradleName>(Default)</gradleName>
    <useWrapper>true</useWrapper>
    <makeExecutable>true</makeExecutable>
    <useWorkspaceAsHome>true</useWorkspaceAsHome>
    <wrapperLocation>.</wrapperLocation>
    <systemProperties>system=1 system2=1</systemProperties>
    <passAllAsSystemProperties>false</passAllAsSystemProperties>
    <projectProperties>project=1 project2=1 xyz=${xyz}</projectProperties>
    <passAllAsProjectProperties>false</passAllAsProjectProperties>
</hudson.plugins.gradle.Gradle>
```

### Transformed Github Action

```yaml
- name: checkout
  uses: actions/checkout@v2
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
    settings-path: "${{ env.{ github.workspace  }}}"
- name: Run Gradle command
  shell: bash
  working_directory: "${{ github.workspace }}/something"
  run: |-
    export GRADLE_USER_HOME=${{ env.GITHUB_WORKSPACE }}
    chmod +x ./gradlew
    ./gradlew -Dsystem=1 -Dsystem2=1 -Pproject=1 -Pproject2=1 -Pxyz=${{ env.xyz }} --build-cache -- console=plain clean install -b something.build

### Unsupported Options

- passAllAsSystemProperties
- passAllAsProjectProperties
