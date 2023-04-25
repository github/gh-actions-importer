# Install Apple Provisioning Profile Task

## Azure DevOps Input

```yaml
- task: InstallAppleProvisioningProfile@1
  inputs:
    provisioningProfileLocation: 'secureFiles'
    provProfileSecureFile: 'Provisioning_Profile.mobileprovision'
```

## Transformed Github Action

```yaml
- name: Install Apple Provisioning Profile
  env:
    BUILD_PROVISION_PROFILE_BASE64: "${{ secrets.BUILD_PROVISION_PROFILE_BASE64 }}"
  run: |
    PP_PATH=$RUNNER_TEMP/build_pp.mobileprovision
    echo -n "$BUILD_PROVISION_PROFILE_BASE64" | base64 --decode --output $PP_PATH
    mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
    cp $PP_PATH ~/Library/MobileDevice/Provisioning\ Profiles
- name: Delete Provision Profile
  run: rm -f ~/Library/MobileDevice/Provisioning\ Profiles/build_pp.mobileprovision
  if: always()
```

## Unsupported Inputs and Aliases
None
