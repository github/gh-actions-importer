# MS Build Task

## Azure DevOps Input

```yaml
- task: MSBuild@1
  inputs:
    solution: '**/*.sln'
    msbuildVersion: '16.0'
    platform: 'platform-field'
    configuration: 'config-field'
    msbuildArguments: 'msbuild args'
    clean: true
    maximumCpuCount: true
    restoreNugetPackages: true
    logProjectEvents: true
    createLogFile: true
    logFileVerbosity: 'minimal'

- task: MSBuild@1
  inputs:
    solution: '**/*.sln'
    msbuildLocationMethod: 'location'
    msbuildLocation: 'path/to/msbuild'
```

## Transformed Github Action

```yaml
- name: install msbuild
  uses: microsoft/setup-msbuild@v1.3.1
- name: run msbuild
  shell: cmd
  run: msbuild solution/*.sln -t:Clean -p:First=value;NewProperty=TestResult;Configuration=Release;Platform="x86";RestorePackagesConfig=true -maxCpuCount -v:minimal -fileLogger

- name: install msbuild
  uses: microsoft/setup-msbuild@v1.3.1
  with:
    vswhere-path: path/to/msbuild
- name: run msbuild
  shell: cmd
  run: msbuild **/*.sln
```

## Unsupported Inputs and Aliases

- msbuildVersion
- logProjectEvents
