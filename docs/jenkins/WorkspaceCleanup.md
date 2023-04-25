# Workspace Cleanup

## Designer Pipeline

### Jenkins Input

```xml
<publishers>
  <hudson.plugins.ws__cleanup.WsCleanup plugin="ws-cleanup@0.38">
    <patterns class="empty-list"/>
    <deleteDirs>true</deleteDirs>
    <skipWhenFailed>false</skipWhenFailed>
    <cleanWhenSuccess>true</cleanWhenSuccess>
    <cleanWhenUnstable>false</cleanWhenUnstable>
    <cleanWhenFailure>false</cleanWhenFailure>
    <cleanWhenNotBuilt>false</cleanWhenNotBuilt>
    <cleanWhenAborted>false</cleanWhenAborted>
    <notFailBuild>true</notFailBuild>
    <cleanupMatrixParent>false</cleanupMatrixParent>
    <externalDelete/>
    <disableDeferredWipeout>false</disableDeferredWipeout>
  </hudson.plugins.ws__cleanup.WsCleanup>
</publishers>
```

```xml
<publishers>
  <hudson.plugins.ws__cleanup.WsCleanup plugin="ws-cleanup@0.38">
    <patterns>
      <hudson.plugins.ws__cleanup.Pattern>
        <pattern>exclude/pattern/</pattern>
        <type>EXCLUDE</type>
      </hudson.plugins.ws__cleanup.Pattern>
      <hudson.plugins.ws__cleanup.Pattern>
        <pattern>include/pattern</pattern>
        <type>INCLUDE</type>
      </hudson.plugins.ws__cleanup.Pattern>
    </patterns>
    <deleteDirs>true</deleteDirs>
    <skipWhenFailed>false</skipWhenFailed>
    <cleanWhenSuccess>true</cleanWhenSuccess>
    <cleanWhenUnstable>false</cleanWhenUnstable>
    <cleanWhenFailure>true</cleanWhenFailure>
    <cleanWhenNotBuilt>false</cleanWhenNotBuilt>
    <cleanWhenAborted>true</cleanWhenAborted>
    <notFailBuild>true</notFailBuild>
    <cleanupMatrixParent>false</cleanupMatrixParent>
    <externalDelete/>
    <disableDeferredWipeout>false</disableDeferredWipeout>
  </hudson.plugins.ws__cleanup.WsCleanup>
</publishers>
```

### Transformed Github Action

```yaml
- name: clean workspace
  shell: bash
  run: rm -rf ${{ github.workspace }}
  continue-on-error: true
  if: success()
```

```yaml
- name: clean workspace
  shell: ruby {0}
  run: |-
    require "fileutils"
    Dir.chdir(ENV["GITHUB_WORKSPACE"]) do
      paths = Dir.glob(["include/pattern"])
      paths -= Dir.glob(["exclude/pattern/"])
      paths.each do |path|
        File.delete(path) if File.file?(path)
        FileUtils.rm_rf(path) if File.directory?(path) # if deleteDirs == true
      end
    end
  continue-on-error: true
  if: always()
```

### Unsupported Options

- External deletion command (externalDelete)
- Disable deferred wipeout (disableDeferredWipeout)
- Clean when not build (cleanWhenNotBuilt)
- Cleanup matrix parent (cleanupMatrixParent)
- Skip when failed (skipWhenFailed)

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
  cleanWs { // Clean after build
      cleanWhenAborted(true)
      cleanWhenFailure(true)
      cleanWhenNotBuilt(false)
      cleanWhenSuccess(true)
      cleanWhenUnstable(true)
      deleteDirs(true)
      notFailBuild(true)
      disableDeferredWipeout(false)
      patterns {
          pattern {
              type('EXCLUDE')
              pattern('.propsfile')
          }
          pattern {
              type('INCLUDE')
              pattern('.gitignore')
          }
      }
  }
}
```

### Transformed Github Action

```yaml
name: clean workspace
shell: ruby {0}
run: |-
  require "fileutils"
  Dir.chdir(ENV["GITHUB_WORKSPACE"]) do
    paths = Dir.glob([".gitignore"])
    paths -= Dir.glob([".propsfile"])
    paths.each do |path|
      File.delete(path) if File.file?(path)
      FileUtils.rm_rf(path) if File.directory?(path)
    end
  end
```

### Unsupported Options

- disableDeferredWipeout
