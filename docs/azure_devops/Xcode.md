# Xcode Task

## Azure DevOps Input

```yaml
- task: Xcode@5
  inputs:
    actions: build test
    packageApp: true
    scheme: ''
    configuration: 'Debug'
    sdk: 'iphoneos'
    useXcpretty: true
    destinationTypeOption: simulators
    destinationPlatformOption: 'iOS'
    destinationSimulators: 'iPad Air (4th generation)'
    exportOptions: plist
    exportOptionsPlist: export.plist

```

## Transformed Github Action

```yaml
- name: Run XcodeBuild Command
  run: "/usr/bin/xcodebuild -sdk iphoneos -configuration Debug -workspace ${{ env.XC_WORKSPACE_PATH }} -scheme ${{ env.SCHEME }} -destination platform='iOS Simulator',name='iPad Air (4th generation)' build testCODE_SIGNING_ALLOWED=NO | xcpretty -r junit --no-color"
  env:
    SCHEME: UPDATE_ME
    XC_WORKSPACE_PATH: UPDATE_ME
- name: Run XcodeBuild Command
  run: "/usr/bin/xcodebuild -sdk iphoneos -configuration Debug -workspace ${{ env.XC_WORKSPACE_PATH }} -scheme ${{ env.SCHEME }} archive -archivePath ${{ env.ARCHIVE_PATH }} CODE_SIGNING_ALLOWED=NO | xcpretty--no-color"
  env:
    SCHEME: UPDATE_ME
    XC_WORKSPACE_PATH: UPDATE_ME
    ARCHIVE_PATH: UPDATE_ME
- name: Run XcodeBuild Export Archive
  run: "/usr/bin/xcodebuild -exportArchive -archivePath ${{ env.ARCHIVE_PATH }} -exportPath output/iphoneos/Debug -exportOptionsPlist export.plist | xcpretty --no-color"
  env:
    ARCHIVE_PATH: UPDATE_ME
```

## Unsupported Inputs and Aliases
- publishJUnitResults
- testRunTitle
- exportOptions: Options `auto` and `specify` are not support. A export plist file is required if packaging
- exportMethod: Should be specified in export plist file
- exportTeamId: Should be specifed in export plist file
