# Custom Tool Install Wrapper

## Designer Pipeline

### Jenkins Input

```xml
<com.cloudbees.jenkins.plugins.customtools.CustomToolInstallWrapper plugin="custom-tools-plugin@0.7">
   <selectedTools>
      <com.cloudbees.jenkins.plugins.customtools.CustomToolInstallWrapper_-SelectedTool>
         <name>NodeJS</name>
      </com.cloudbees.jenkins.plugins.customtools.CustomToolInstallWrapper_-SelectedTool>
   </selectedTools>
   <multiconfigOptions>
      <skipMasterInstallation>false</skipMasterInstallation>
   </multiconfigOptions>
   <convertHomesToUppercase>false</convertHomesToUppercase>
</com.cloudbees.jenkins.plugins.customtools.CustomToolInstallWrapper>
```

### Transformed Github Action

N/A

### Manual Tasks

- Any referenced tool will be listed as manual steps to be installed in the migration pull request

### Unsupported Options

The following run conditions are not supported:

- Don't install tools at the master job
- Convert #ToolName_HOME variables to the upper-case

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
