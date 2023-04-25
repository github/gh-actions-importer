# Xamarin Android Task

## Azure DevOps Input

```yaml
- task: XamarinAndroid@1
  inputs:
    project: "**/*.csproj"                      #Required, Defaults to "**/*.csproj"
    #target: "Target1;Target2"                  #Optional
    #outputDir: "path/to/output"                #Optional
    #configuration: "Debug"                     #Optional
    #createAppPackage: false                    #Optional, Defaults to true
    #clean: true                                #Optional, Defaults to false
    #msbuildLocationMethod: "version"           #Optional, Defaults to "version"
    #msbuildVersion: "16.0"                     #Optional, Defaults to "15.0"
    #msbuildLocation: "path/to/msbuild"         #Required when msbuildLocationMethod == "version"
    #msbuildArchitecture: "x86"                 #Optional, Defaults to "x86"
    #msbuildArguments: "args"                   #Optional
    jdkSelection: "JDKVersion"                  #Required, Defaults to "JDKVersion"
    jdkVersion: "1.8"                          #Optional, Defaults to "default"
    #jdkUserInputPath: "path/to/jdk"            #Required when jdkSelection == "Path"
    #jdkArchitecture: "x86"                     #Optional, Defaults to "x64

```

## Transformed Github Action

```yaml
- name: Install Java Version 8
  uses: actions/setup-java@v3.10.0
  with:
    distribution: zulu
    java-version: '8'
- name: Build Xamarin Project
  run: msbuild ${{ env.PROJECT_PATH }} /t:PackageForAndroid /p:JavaSdkDirectory="${{ env.JAVA_HOME }}"
  env:
    PROJECT_PATH: UPDATE_ME
```

## Unsupported Inputs and Aliases
- msbuildVersion (On macos runners)
- msbuildArchitecture
