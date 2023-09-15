# Azure Storage Deploy

[BitBucket Azure Storage Deploy Documentation](https://bitbucket.org/atlassian/azure-storage-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/azure-storage-deploy:1.1.0
  variables:
    SOURCE: 'mydirectory'
    DESTINATION: 'https://mystorageaccount.blob.core.windows.net/mycontainer/mydirectory'
    DESTINATION_SAS_TOKEN: $AZURE_STORAGE_SAS_TOKEN
    EXTRA_ARGS: '--overwrite=false'
    DEBUG: 'true'
```

## Transformed GitHub Action

```yaml
- name: Copy with azcopy
  shell: bash
  env:
    SOURCE: mydirectory
    DESTINATION: https://mystorageaccount.blob.core.windows.net/mycontainer/mydirectory$AZURE_STORAGE_SAS_TOKEN
    EXTRA_ARGS: "--overwrite=false"
    DEBUG: "--log-level=DEBUG"
  run: |
    if [ -d "${SOURCE}" ]; then
      recursive_flag="--recursive=true"
    fi
    azcopy copy "${{ env.SOURCE }}" "${{ env.DESTINATION }}" ${{ env.EXTRA_ARGS }} ${{ env.DEBUG }} $recursive_flag
```

## Unsupported Options
- N/A