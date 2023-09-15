# Ftp Deploy

[BitBucket Ftp Deploy Documentation](https://bitbucket.org/atlassian/ftp-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/ftp-deploy:0.5.0
  variables:
    USER: my-ftp-user
    PASSWORD: $FTP_PASSWORD
    SERVER: 127.0.0.1
    REMOTE_PATH: /tmp/my-remote-directory
```

## Transformed GitHub Action
```yaml
- name: FTP Deploy
  env:
    USER: my-ftp-user
    PASSWORD: "$FTP_PASSWORD"
    SERVER: 127.0.0.1
    REMOTE_PATH: "/tmp/my-remote-directory"
    LOCAL_PATH: "${{ github.workspace }}"
    SET_ARGS: ftp:ssl-allow no
    EXTRA_ARGS: "--delete-first"
  run: lftp -u $USER,$PASSWORD -e "set ${SET_ARGS}; mirror ${EXTRA_ARGS} -R ${LOCAL_PATH} ${REMOTE_PATH};quit" $SERVER
```

## Unsupported Options
N/A