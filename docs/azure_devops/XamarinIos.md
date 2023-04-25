# Xamarin Ios Task

## Azure DevOps Input

```yaml
- task: XamariniOS@2
  inputs:
    solutionFile: '**/*iOS.csproj'
    configuration: 'Release'
    clean: true
    packageApp: true
    buildForSimulator: true
    runNugetRestore: true
    args: '-noLogo'
```

## Transformed Github Action

```yaml
- name: Xamarin Clean Build
  run: msbuild ${{ env.SOLUTION_PATH }} /p:Configuration=Release /p:Platform=iPhoneSimulator -noLogo /t:Clean
  env:
    SOLUTION_PATH: UPDATE_ME
- name: Xamarin Nuget Restore
  run: nuget restore ${{ env.SOLUTION_PATH }}
  env:
    SOLUTION_PATH: UPDATE_ME
- name: Xamarin Build
  run: msbuild ${{ env.SOLUTION_PATH }} /p:Configuration=Release /p:Platform=iPhoneSimulator /p:BuildIpa=true -noLogo
  env:
    SOLUTION_PATH: UPDATE_ME
```

## Unsupported Inputs and Aliases
