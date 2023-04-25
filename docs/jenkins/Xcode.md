# Xcode

## Designer Pipeline

### Jenkins Inputs
#### Xcode
```xml
<builders>
    <au.com.rayh.XCodeBuilder plugin="xcode-plugin@2.0.15">
        <cleanBeforeBuild>false</cleanBeforeBuild>
        <cleanTestReports>false</cleanTestReports>
        <configuration>Release</configuration>
        <target/>
        <sdk/>
        <symRoot/>
        <buildDir/>
        <xcodeProjectPath/>
        <xcodeProjectFile/>
        <xcodebuildArguments/>
        <xcodeSchema>ios-action-test</xcodeSchema>
        <xcodeWorkspaceFile/>
        <cfBundleVersionValue>tech_version</cfBundleVersionValue>
        <cfBundleShortVersionStringValue>mark_version</cfBundleShortVersionStringValue>
        <buildIpa>true</buildIpa>
        <ipaExportMethod>development</ipaExportMethod>
        <generateArchive>true</generateArchive>
        <noConsoleLog>false</noConsoleLog>
        <logfileOutputDirectory/>
        <unlockKeychain>false</unlockKeychain>
        <keychainId/>
        <keychainPath/>
        <keychainPwd>{AQAAABAAAAAQd8SEznPb3jmuLxdb31GBvs/Vz9hwpOehxdJu8czPbgg=}</keychainPwd>
        <developmentTeamName>team_gh</developmentTeamName>
        <developmentTeamID/>
        <allowFailingBuildResults>false</allowFailingBuildResults>
        <ipaName>my_ipa</ipaName>
        <ipaOutputDirectory>results</ipaOutputDirectory>
        <provideApplicationVersion>false</provideApplicationVersion>
        <changeBundleID>false</changeBundleID>
        <bundleID/>
        <bundleIDInfoPlistPath/>
        <interpretTargetAsRegEx>false</interpretTargetAsRegEx>
        <signingMethod>automatic</signingMethod>
        <provisioningProfiles>
            <au.com.rayh.ProvisioningProfile>
                <provisioningProfileAppId>pro_bundle_ID</provisioningProfileAppId>
                <provisioningProfileUUID>pro_UUID</provisioningProfileUUID>
            </au.com.rayh.ProvisioningProfile>
        </provisioningProfiles>
        <uploadBitcode>true</uploadBitcode>
        <uploadSymbols>true</uploadSymbols>
        <compileBitcode>true</compileBitcode>
        <thinning/>
        <appURL>app url here</appURL>
        <displayImageURL>image url here</displayImageURL>
        <fullSizeImageURL>full size url here</fullSizeImageURL>
        <assetPackManifestURL>asset url</assetPackManifestURL>
        <skipBuildStep>false</skipBuildStep>
        <stripSwiftSymbols>true</stripSwiftSymbols>
        <copyProvisioningProfile>true</copyProvisioningProfile>
        <useLegacyBuildSystem>false</useLegacyBuildSystem>
        <resultBundlePath/>
        <cleanResultBundlePath>false</cleanResultBundlePath>
    </au.com.rayh.XCodeBuilder>
</builders>
```

#### Export IPA

```xml
<au.com.rayh.ExportIpa plugin="xcode-plugin@2.0.14">
    <xcodeProjectPath/>
    <unlockKeychain>false</unlockKeychain>
    <keychainName/>
    <keychainPath/>
    <keychainPwd>{AQAAABAAAAAQoqXOCjipOxFD6M+De6oJ9H8AHkhLXmC73mpoiPLLFEA=}</keychainPwd>
    <xcodeWorkspaceFile/>
    <xcodeSchema>test_schema</xcodeSchema>
    <archiveDir>${WORKSPACE}</archiveDir>
    <developmentTeamName/>
    <developmentTeamID>1234</developmentTeamID>
    <ipaName>myapp</ipaName>
    <ipaOutputDirectory>artifacts</ipaOutputDirectory>
    <ipaExportMethod>development</ipaExportMethod>
    <signingMethod>automatic</signingMethod>
    <provisioningProfiles>
        <au.com.rayh.ProvisioningProfile>
            <provisioningProfileAppId/>
            <provisioningProfileUUID/>
        </au.com.rayh.ProvisioningProfile>
    </provisioningProfiles>
    <uploadBitcode>true</uploadBitcode>
    <uploadSymbols>true</uploadSymbols>
    <compileBitcode>true</compileBitcode>
    <thinning/>
    <embedOnDemandResourcesAssetPacksInBundle>true</embedOnDemandResourcesAssetPacksInBundle>
    <onDemandResourcesAssetPacksBaseURL/>
    <appURL/>
    <displayImageURL/>
    <fullSizeImageURL/>
    <assetPackManifestURL/>
    <stripSwiftSymbols>true</stripSwiftSymbols>
    <copyProvisioningProfile>true</copyProvisioningProfile>
</au.com.rayh.ExportIpa>
```

#### Import Developer Profile

```xml
<au.com.rayh.DeveloperProfileLoader plugin="xcode-plugin@2.0.14">
    <importIntoExistingKeychain>false</importIntoExistingKeychain>
    <keychainId/>
    <keychainPath/>
    <keychainPwd>{AQAAABAAAAAQh3hpE2USsA3h3fuz/r6l3R803rx5m25Elf7tupXKI7Q=}</keychainPwd>
</au.com.rayh.DeveloperProfileLoader>
```

#### Unlock macOS X Keychain

```xml
<au.com.rayh.KeychainUnlockStep plugin="xcode-plugin@2.0.14">
    <keychainId>ios_keychain</keychainId>
    <keychainPath/>
    <keychainPwd>{AQAAABAAAAAQW/seUVsJtOqgX9xuwNHYJlQr+J0y60VcddZ8zMkvh/8=}</keychainPwd>
</au.com.rayh.KeychainUnlockStep>
```
### Transformed Github Actions

#### Xcode

```yaml
name: xcode
on:
  workflow_dispatch:
jobs:
  build:
    runs-on:
      - macos-latest
    steps:
    - name: checkout
      uses: actions/checkout@v2
    # If using a self-hosted runner, ensure the runner’s keychain is cleaned up at the end of the build
    - name: Install the Apple certificate and provisioning profile
      env:
        BUILD_CERTIFICATE_BASE64: "${{ secrets.BUILD_CERTIFICATE_BASE64 }}"
        P12_PASSWORD: "${{ secrets.P12_PASSWORD }}"
        BUILD_PROVISION_PROFILE_BASE64: "${{ secrets.BUILD_PROVISION_PROFILE_BASE64 }}"
        KEYCHAIN_PASSWORD: "${{ secrets.KEYCHAIN_PASSWORD }}"
      run: |
        CERTIFICATE_PATH=$RUNNER_TEMP/build_certificate.p12
        PP_PATH=$RUNNER_TEMP/build_pp.mobileprovision
        KEYCHAIN_PATH=$RUNNER_TEMP/app-signing.keychain-db
        # import certificate and provisioning profile from secrets
        echo -n "$BUILD_CERTIFICATE_BASE64" | base64 --decode --output $CERTIFICATE_PATH
        echo -n "$BUILD_PROVISION_PROFILE_BASE64" | base64 --decode --output $PP_PATH
        # create temporary keychain
        security create-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
        security set-keychain-settings -lut 21600 $KEYCHAIN_PATH
        security unlock-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
        # import certificate to keychain
        security import $CERTIFICATE_PATH -P "$P12_PASSWORD" -A -t cert -f pkcs12 -k $KEYCHAIN_PATH
        security list-keychain -d user -s $KEYCHAIN_PATH
        # apply provisioning profile
        mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
        cp $PP_PATH ~/Library/MobileDevice/Provisioning\ Profiles
    - name: Xcode Build/Archive
      run: "/usr/bin/xcodebuild -alltargets -scheme ios-action-test -configuration Release archive -archivePath ./build/Release-iphoneos/ios-action-test.xcarchive DEVELOPMENT_TEAM=${{ env.DEVELOPMENT_TEAM_ID }}"
      env:
        DEVELOPMENT_TEAM_ID: UPDATE_ME
    - name: Xcode Pack, Build, and Sign .ipa
      run: "/usr/bin/xcodebuild -exportArchive -archivePath ./build/Release-iphoneos/ios-action-test.xcarchive -exportPath ./build/Release-iphoneos/results -exportOptionsPlist ${{ env.ExportOptionsPlistPath }}"
      env:
        ExportOptionsPlistPath: UPDATE_ME

```


#### Export IPA

```yaml
# If using a self-hosted runner, ensure the runner’s keychain is cleaned up at the end of the build
- name: Install the Apple certificate and provisioning profile
  env:
    BUILD_CERTIFICATE_BASE64: "${{ secrets.BUILD_CERTIFICATE_BASE64 }}"
    P12_PASSWORD: "${{ secrets.P12_PASSWORD }}"
    BUILD_PROVISION_PROFILE_BASE64: "${{ secrets.BUILD_PROVISION_PROFILE_BASE64 }}"
    KEYCHAIN_PASSWORD: "${{ secrets.KEYCHAIN_PASSWORD }}"
  run: |
    CERTIFICATE_PATH=$RUNNER_TEMP/build_certificate.p12
    PP_PATH=$RUNNER_TEMP/build_pp.mobileprovision
    KEYCHAIN_PATH=$RUNNER_TEMP/app-signing.keychain-db
    # import certificate and provisioning profile from secrets
    echo -n "$BUILD_CERTIFICATE_BASE64" | base64 --decode --output $CERTIFICATE_PATH
    echo -n "$BUILD_PROVISION_PROFILE_BASE64" | base64 --decode --output $PP_PATH
    # create temporary keychain
    security create-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    security set-keychain-settings -lut 21600 $KEYCHAIN_PATH
    security unlock-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    # import certificate to keychain
    security import $CERTIFICATE_PATH -P "$P12_PASSWORD" -A -t cert -f pkcs12 -k $KEYCHAIN_PATH
    security list-keychain -d user -s $KEYCHAIN_PATH
    # apply provisioning profile
    mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
    cp $PP_PATH ~/Library/MobileDevice/Provisioning\ Profiles
- name: Xcode Pack, Build, and Sign .ipa
  run: "/usr/bin/xcodebuild -exportArchive -archivePath ./test_schema.xcarchive -exportPath ./artifacts -exportOptionsPlist ${{ env.ExportOptionsPlistPath }}"
  env:
    ExportOptionsPlistPath: UPDATE_ME
```

#### Import Developer Profile

```yaml
# If using a self-hosted runner, ensure the runner’s keychain is cleaned up at the end of the build
- name: Install the Apple certificate and provisioning profile
  env:
    BUILD_CERTIFICATE_BASE64: "${{ secrets.BUILD_CERTIFICATE_BASE64 }}"
    P12_PASSWORD: "${{ secrets.P12_PASSWORD }}"
    BUILD_PROVISION_PROFILE_BASE64: "${{ secrets.BUILD_PROVISION_PROFILE_BASE64 }}"
    KEYCHAIN_PASSWORD: "${{ secrets.KEYCHAIN_PASSWORD }}"
  run: |
    CERTIFICATE_PATH=$RUNNER_TEMP/build_certificate.p12
    PP_PATH=$RUNNER_TEMP/build_pp.mobileprovision
    KEYCHAIN_PATH=$RUNNER_TEMP/app-signing.keychain-db
    # import certificate and provisioning profile from secrets
    echo -n "$BUILD_CERTIFICATE_BASE64" | base64 --decode --output $CERTIFICATE_PATH
    echo -n "$BUILD_PROVISION_PROFILE_BASE64" | base64 --decode --output $PP_PATH
    # create temporary keychain
    security create-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    security set-keychain-settings -lut 21600 $KEYCHAIN_PATH
    security unlock-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    # import certificate to keychain
    security import $CERTIFICATE_PATH -P "$P12_PASSWORD" -A -t cert -f pkcs12 -k $KEYCHAIN_PATH
    security list-keychain -d user -s $KEYCHAIN_PATH
    # apply provisioning profile
    mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
    cp $PP_PATH ~/Library/MobileDevice/Provisioning\ Profiles
```

#### Unlock macOS X Keychain

```yaml
- name: Unlock Keychain
  run: security unlock-keychain -p ${{ secrets.KEYCHAIN_PASSWORD }} $KEYCHAIN_PATH
  env:
    KEYCHAIN_PATH: UPDATE_ME
```

### Unsupported

The following options are not supported:
- logfileOutputDirectory
- allowFailingBuildResults
- ipaName
- interpretTargetAsRegEx
- useLegacyBuildSystem
- cleanResultBundlePath
- noConsoleLog

The following options are assumed to be set in plist file located at ExportOptionsPlistPath
- ipaExportMethod
- provisioningProfiles
- uploadBitcode
- uploadSymbols
- compileBitcode
- thinning
- appURL
- displayImageURL
- fullSizeImageURL
- assetPackManifestURL
- skipBuildStep
- stripSwiftSymbols
- copyProvisioningProfile


## Jenkinsfile Pipeline

### Jenkins Input

#### xcodeBuild
```groovy
steps {
  xcodeBuild appURL: '',
    assetPackManifestURL: '',
    buildDir: '',
    buildIpa: true,
    bundleID: 'new_bundle_id', 
    bundleIDInfoPlistPath: './ios-action-test/Info.plist',
    changeBundleID: false,
    cfBundleShortVersionStringValue: 'mark_ver1',
    cfBundleVersionValue: 'ver2',
    provideApplicationVersion: true,
    cleanBeforeBuild: false,
    cleanResultBundlePath: false,
    configuration: 'Release',
    developmentTeamID: '123',
    developmentTeamName: '',
    displayImageURL: '', 
    fullSizeImageURL: '',
    generateArchive: true,
    ipaExportMethod: 'developement',
    ipaName: '',
    ipaOutputDirectory: 'output',
    keychainId: '',
    keychainPath: '',
    logfileOutputDirectory: '',
    provisioningProfiles: [[provisioningProfileAppId: '', provisioningProfileUUID: '']],
    resultBundlePath: '',
    sdk: '',
    symRoot: '',
    target: '',
    thinning: '',
    xcodeProjectFile: '',
    xcodeProjectPath: '',
    xcodeSchema: 'ios-action-test',
    xcodeWorkspaceFile: '',
    xcodebuildArguments: ''
  }
```
#### exportIpa
```groovy
step {
 exportIpa appURL: '',
  archiveDir: '${WORKSPACE}', 
  assetPackManifestURL: '', 
  compileBitcode: true, 
  developmentTeamID: '123', 
  developmentTeamName: '', 
  displayImageURL: '', 
  fullSizeImageURL: '', 
  ipaExportMethod: 'development', 
  ipaName: 'test_pattern', 
  ipaOutputDirectory: 'output', 
  keychainName: '', 
  keychainPath: '', 
  manualSigning: false, 
  packResourcesAsset: true, 
  provisioningProfiles: [[provisioningProfileAppId: '', provisioningProfileUUID: '']], 
  resourcesAssetURL: '', 
  thinning: '', 
  unlockKeychain: false, 
  uploadBitcode: true, 
  uploadSymbols: true, 
  xcodeProjectPath: '', 
  xcodeSchema: 'ios-action-test', 
  xcodeWorkspaceFile: ''
}
```

#### importDeveloperProfile
```groovy
stage('import_profile') {
    steps {
        importDeveloperProfile keychainId: 'my_keychain'
    }
}
```

#### unlockMacOSKeychain
```groovy
stage('unlock_keychain') {
  steps {
      unlockMacOSKeychain keychainId: '', keychainPath: '/Users/jenkins/login.keychain.db'
  }
}
```


### Transformed Github Action

#### xcodeBuild
```yaml
- name: Install the Apple certificate and provisioning profile
  env:
    BUILD_CERTIFICATE_BASE64: "${{ secrets.BUILD_CERTIFICATE_BASE64 }}"
    P12_PASSWORD: "${{ secrets.P12_PASSWORD }}"
    BUILD_PROVISION_PROFILE_BASE64: "${{ secrets.BUILD_PROVISION_PROFILE_BASE64 }}"
    KEYCHAIN_PASSWORD: "${{ secrets.KEYCHAIN_PASSWORD }}"
  run: |
    CERTIFICATE_PATH=$RUNNER_TEMP/build_certificate.p12
    PP_PATH=$RUNNER_TEMP/build_pp.mobileprovision
    KEYCHAIN_PATH=$RUNNER_TEMP/app-signing.keychain-db
    # import certificate and provisioning profile from secrets
    echo -n "$BUILD_CERTIFICATE_BASE64" | base64 --decode --output $CERTIFICATE_PATH
    echo -n "$BUILD_PROVISION_PROFILE_BASE64" | base64 --decode --output $PP_PATH
    # create temporary keychain
    security create-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    security set-keychain-settings -lut 21600 $KEYCHAIN_PATH
    security unlock-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    # import certificate to keychain
    security import $CERTIFICATE_PATH -P "$P12_PASSWORD" -A -t cert -f pkcs12 -k $KEYCHAIN_PATH
    security list-keychain -d user -s $KEYCHAIN_PATH
    # apply provisioning profile
    mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
    cp $PP_PATH ~/Library/MobileDevice/Provisioning\ Profiles
- name: Xcode Build/Archive
  run: |-
    /usr/bin/agvtool new-version -all 'ver2'
    /usr/bin/agvtool new-marketing-version 'mark_ver1'
    /usr/bin/xcodebuild -alltargets -scheme ios-action-test -configuration Release archive -archivePath ./build/Release-iphoneos/ios-action-test.xcarchive DEVELOPMENT_TEAM=123
- name: Xcode Pack, Build, and Sign .ipa
  run: "/usr/bin/xcodebuild -exportArchive -archivePath ./build/Release-iphoneos/ios-action-test.xcarchive -exportPath ./build/Release-iphoneos/output -exportOptionsPlist ${{ env.ExportOptionsPlistPath }}"
  env:
    ExportOptionsPlistPath: UPDATE_ME
```

#### exportIpa

```yaml
- name: Install the Apple certificate and provisioning profile
  env:
    BUILD_CERTIFICATE_BASE64: "${{ secrets.BUILD_CERTIFICATE_BASE64 }}"
    P12_PASSWORD: "${{ secrets.P12_PASSWORD }}"
    BUILD_PROVISION_PROFILE_BASE64: "${{ secrets.BUILD_PROVISION_PROFILE_BASE64 }}"
    KEYCHAIN_PASSWORD: "${{ secrets.KEYCHAIN_PASSWORD }}"
  run: |
    CERTIFICATE_PATH=$RUNNER_TEMP/build_certificate.p12
    PP_PATH=$RUNNER_TEMP/build_pp.mobileprovision
    KEYCHAIN_PATH=$RUNNER_TEMP/app-signing.keychain-db
    # import certificate and provisioning profile from secrets
    echo -n "$BUILD_CERTIFICATE_BASE64" | base64 --decode --output $CERTIFICATE_PATH
    echo -n "$BUILD_PROVISION_PROFILE_BASE64" | base64 --decode --output $PP_PATH
    # create temporary keychain
    security create-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    security set-keychain-settings -lut 21600 $KEYCHAIN_PATH
    security unlock-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    # import certificate to keychain
    security import $CERTIFICATE_PATH -P "$P12_PASSWORD" -A -t cert -f pkcs12 -k $KEYCHAIN_PATH
    security list-keychain -d user -s $KEYCHAIN_PATH
    # apply provisioning profile
    mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
    cp $PP_PATH ~/Library/MobileDevice/Provisioning\ Profiles
- name: Xcode Pack, Build, and Sign .ipa
  run: "/usr/bin/xcodebuild -exportArchive -archivePath ./ios-action-test.xcarchive -exportPath ./output -exportOptionsPlist ${{ env.ExportOptionsPlistPath }}"
  env:
    ExportOptionsPlistPath: UPDATE_ME
```

#### importDeveloperProfile

```yaml
# If using a self-hosted runner, ensure the runner’s keychain is cleaned up at the end of the build
- name: Install the Apple certificate and provisioning profile
  env:
    BUILD_CERTIFICATE_BASE64: "${{ secrets.BUILD_CERTIFICATE_BASE64 }}"
    P12_PASSWORD: "${{ secrets.P12_PASSWORD }}"
    BUILD_PROVISION_PROFILE_BASE64: "${{ secrets.BUILD_PROVISION_PROFILE_BASE64 }}"
    KEYCHAIN_PASSWORD: "${{ secrets.KEYCHAIN_PASSWORD }}"
  run: |
    CERTIFICATE_PATH=$RUNNER_TEMP/build_certificate.p12
    PP_PATH=$RUNNER_TEMP/build_pp.mobileprovision
    KEYCHAIN_PATH=$RUNNER_TEMP/app-signing.keychain-db
    # import certificate and provisioning profile from secrets
    echo -n "$BUILD_CERTIFICATE_BASE64" | base64 --decode --output $CERTIFICATE_PATH
    echo -n "$BUILD_PROVISION_PROFILE_BASE64" | base64 --decode --output $PP_PATH
    # create temporary keychain
    security create-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    security set-keychain-settings -lut 21600 $KEYCHAIN_PATH
    security unlock-keychain -p "$KEYCHAIN_PASSWORD" $KEYCHAIN_PATH
    # import certificate to keychain
    security import $CERTIFICATE_PATH -P "$P12_PASSWORD" -A -t cert -f pkcs12 -k $KEYCHAIN_PATH
    security list-keychain -d user -s $KEYCHAIN_PATH
    # apply provisioning profile
    mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
    cp $PP_PATH ~/Library/MobileDevice/Provisioning\ Profiles
```

#### unlockMacOSKeychain

```yaml
- name: Unlock Keychain
  run: security unlock-keychain -p ${{ secrets.KEYCHAIN_PASSWOR}} $KEYCHAIN_PATH
  env:
    KEYCHAIN_PATH: "/Users/jenkins/login.keychain.db"
```


### Unsupported Options
The following options are not supported:
- logfileOutputDirectory
- allowFailingBuildResults
- ipaName
- interpretTargetAsRegEx
- useLegacyBuildSystem
- cleanResultBundlePath
- noConsoleLog

The following options are assumed to be set in plist file located at ExportOptionsPlistPath
- ipaExportMethod
- provisioningProfiles
- uploadBitcode
- uploadSymbols
- compileBitcode
- thinning
- appURL
- displayImageURL
- fullSizeImageURL
- assetPackManifestURL
- skipBuildStep
- stripSwiftSymbols
- copyProvisioningProfile
