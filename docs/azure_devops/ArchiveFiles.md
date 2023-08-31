# ArchiveFiles Task

## Azure DevOps Input

```yaml
steps:
- task: ArchiveFiles@2
  inputs:
    rootFolderOrFile: '$(Build.BinariesDirectory)'  # Required, Default: $(Build.BinariesDirectory)
    includeRootFolder: true                         # Required
    archiveType: 'zip'                              # Required, Options: zip, 7z, tar, wim
    tarCompression: 'gz'                            # Optional, Options: gz, bz2, xz, None.  Default: 'gz'
    sevenZipCompression: "9"                        # Default: 5
    archiveFile: 'archive_file'                     # Required, Default: $(Build.ArtifactStagingDirectory)/$(Build.BuildId).zip
    replaceExistingArchive: true                    # Required
    verbose: false                                  # Optional
    quiet: false                                    # Optional
```

### Transformed Github Action

Note:

- incoming input for archive_file needs to specify extension type. It is not added in transformation.
- Zip uses 7z compression

```yaml

- name: Tar files
  run: tar -cf archive_file $(Build.BinariesDirectory) --overwrite
```

```yaml

- name: Zip files
  run: 7z u archive_file $(Build.BinariesDirectory)
```

```yaml

- name: 7z files
  run: 7z u -mx=9 archive_file $(Build.BinariesDirectory)
```

### Unsupported Inputs

- archiveType (wim)
- quiet
- verbose (7z, zip)
- sevenzipcompression (zip)
