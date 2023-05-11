# Visual Studio

## Bamboo input

```yaml
Default Job:
  key: JOB1
  tasks:
  - any-task:
      plugin-key: com.atlassian.bamboo.plugin.dotnet:devenv
      configuration:
        solution: ActionsImporter.sln
        options: /p:Configuration=Release /p:platform="Any CPU"
        label: my-exe
        vsEnvironment: amd64
      description: Build VS Task
```

## Transformed Github Action

```yaml
jobs:
  Default-Stage-Default-Job:
    runs-on: windows-latest
    steps:
    - uses: seanmiddleditch/gha-setup-vsdevenv@v4
      with:
        arch: amd64
    - name: Run Visual Studio
      run: devenv amd64 ActionsImporter.sln /p:Configuration=Release /p:platform="Any CPU"
```

## Unsupported Options

* label
* description
