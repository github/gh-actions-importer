# PreBuild Cleanup

## Designer Pipeline

### Jenkins Input

```xml
<buildWrappers>
  <hudson.plugins.ws__cleanup.PreBuildCleanup plugin="ws-cleanup@0.38">
    <patterns>
      <hudson.plugins.ws__cleanup.Pattern>
        <pattern>**/*.rb</pattern>
        <type>INCLUDE</type>
      </hudson.plugins.ws__cleanup.Pattern>
      <hudson.plugins.ws__cleanup.Pattern>
        <pattern>**/*.ts</pattern>
        <type>EXCLUDE</type>
      </hudson.plugins.ws__cleanup.Pattern>
    </patterns>
    <deleteDirs>true</deleteDirs>
    <cleanupParameter/>
    <externalDelete/>
    <disableDeferredWipeout>false</disableDeferredWipeout>
  </hudson.plugins.ws__cleanup.PreBuildCleanup>
</buildWrappers>
```

### Transformed Github Action

```yaml
name: clean workspace
shell: bash
run: rm -rf ${{ github.workspace }}/*
```

```yaml
name: clean workspace
shell: ruby {0}
run: |-
  require "fileutils"
  Dir.chdir(ENV["GITHUB_WORKSPACE"]) do
    paths = Dir.glob(["**/*.rb"])
    paths -= Dir.glob(["**/*.ts"])
    paths.each do |path|
      File.delete(path) if File.file?(path)
      FileUtils.rm_rf(path) if File.directory?(path)
    end
  end
```

### Unsupported Options

- Check parameter (cleanupParameter)
- External Deletion Command (externalDelete)
- Disable deferred wipeout (disableDeferredWipeout)

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
