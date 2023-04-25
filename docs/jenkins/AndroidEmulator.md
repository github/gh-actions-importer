# Android Emulator

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.android__emulator.AndroidEmulator plugin="android-emulator@3.1.2">
  <osVersion>4.1</osVersion>
  <screenDensity>240</screenDensity>
  <targetAbi>armeabi-v7a</targetAbi>
  <sdCardSize>64M</sdCardSize>
  <wipeData>true</wipeData>
  <startupTimeout>10</startupTimeout>
  <commandLineOptions>-netfast -netspeed full -port 5556</commandLineOptions>
</hudson.plugins.android__emulator.AndroidEmulator>
```

### Transformed Github Action

```yaml
- name: run tests on android emulator
  uses: reactivecircus/android-emulator-runner@v2
  with:
    api-level: 16
    arch: armeabi-v7a
    sdcard-path-or-size: 64M
    emulator-options: "-netfast -netspeed full -port 5556 -wipe-data"
```

### Unsupported Options

- screenDensity
- screenResolution
- deviceDefinition
- deviceLocale
- avdNameSuffix
- hardwareProperties
- ShowWindow
- UseSnapshots
- deleteAfterBuild
- startupDelay
- startupTimeout
