# Windows Machine File Copy Task

## Azure DevOps Input

```yaml
- task: WindowsMachineFileCopy@2
  inputs:
    SourcePath: 'copy_me'
    MachineNames: 'testing.westus.cloudapp.azure.com'
    AdminUserName: 'matz'
    AdminPassword: 'P@ssW0rd'
    TargetPath: 'C:\Users\matz\Desktop\testing'
    CleanTargetBeforeCopy: true
    CopyFilesInParallel: false
    AdditionalArguments: '/pf'
```

## Transformed Github Action

```yaml
- name: Copy Files to Machine File Share
  shell: pwsh
  run: |-
    $sourcePath = "${{ env.SOURCE_PATH }}"
    $robocopyParameters="${{ env.ADDITIONAL_COPY_ARGS }}"
    $isFileCopy = Test-Path -Path $sourcePath -PathType Leaf
    if($isFileCopy) {
      $sourceDirectory = Split-Path $sourcePath
      $filesToCopy = Split-Path $sourcePath -Leaf
      if(-not $sourceDirectory){ $sourceDirectory = "." }
    }
    else {
      $sourceDirectory = $sourcePath
      $filesToCopy = ""
      $robocopyParameters += " /E"
    }
    # mount machine file share
    $psCredentialObject = New-Object pscredential -ArgumentList "${{ env.USER }}", (ConvertTo-SecureString -String "${{ env.PASSWORD }}" -AsPlainText -Force)
    New-PSDrive -Name 'WFCPSDrive' -PSProvider FileSystem -Root "${{ env.FILE_SHARE }}" -Credential $psCredentialObject -ErrorAction "Stop"
    # create missing target directories
    New-Item -ItemType Directory ${{ env.TARGET_PATH }} -ErrorAction 'Stop' -Force
    # clean target directory
    if("${{ env.CLEAN_DESTINATION}}" -eq "true") {
      $tempDirectory = "${{ runner.temp }}/clean_up"
      New-Item -ItemType Directory -Force -Path $tempDirectory
      Invoke-Expression "robocopy `"$tempDirectory`" `"${{ env.TARGET_PATH }}`" `"*.*`" /NOCOPY /E /PURGE"
      Remove-Item $tempDirectory -Recurse -ErrorAction Ignore
    }
    # copy files
    Invoke-Expression "robocopy `"$sourceDirectory`" `"${{ env.TARGET_PATH }}`" `"$filesToCopy`" $robocopyParameters"
    # robocopy exitcodes of 0 thru 8 are considered successful
    $copyExitCode = ($LASTEXITCODE -ge 8) ? 1 : 0
    # remove file share
    Remove-PSDrive -Name "WFCPSDrive" -ErrorAction SilentlyContinue
    exit $copyExitCode
  env:
    USER: matz
    PASSWORD: "${{ secrets.FILE_SHARE_USER_PASSWORD }}"
    SOURCE_PATH: copy_me
    TARGET_PATH: "\\\\testing.westus.cloudapp.azure.com\\C$\\Users\\matz\\Desktop\\testing"
    FILE_SHARE: "\\\\testing.westus.cloudapp.azure.com\\C$\\Users\\matz\\Desktop\\testing"
    ADDITIONAL_COPY_ARGS: "/COPY:DAT /pf"
    CLEAN_DESTINATION: true
```

## Unsupported Inputs and Aliases
- Copy Files in Parallel
- Machine Name Filter (only available in version 1)
