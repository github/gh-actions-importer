# Sql Azure Dacpac Deployment Task

## Azure DevOps Input

```yaml
- task: SSISBuild@1
  displayName: 'Build SSIS'
  inputs:
    projectPath: folder/containing/projects
    projectPassword: hunter12
    stripSensitive: true

```

## Transformed Github Action

```yaml
- name: Build SSIS
  run: |-
    Get-ChildItem -Path folder/containing/projects -Filter *.dtproj -Recurse -File -Name| ForEach-Object {
      SSISBuild.exe -project:$_ -projectPassword:hunter12 -stripSensitive -output:${{ runner.temp }}
    }
  shell: pwsh
```

## Additional Information

- This script relies on the `SSISBuild.exe` executable being present on the runner. Instructions to install this can be found [here](https://docs.microsoft.com/en-us/sql/integration-services/devops/ssis-devops-standalone?view=sql-server-ver16#installation).
