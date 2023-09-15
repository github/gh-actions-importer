# Sftp Deploy

[BitBucket Sftp Deploy Documentation](https://bitbucket.org/atlassian/sftp-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/sftp-deploy:0.6.0
variables:
  USER: 'ec2-user'
  SERVER: '127.0.0.1'
  REMOTE_PATH: '/var/www/build/'
  LOCAL_PATH: 'build'
  DEBUG: 'true'
  EXTRA_ARGS: '-P 22324'
```

## Transformed GitHub Action
```yaml
- name: Install SSH key
  uses: shimataro/ssh-key-action@v2.5.0
  with:
    key: "${{ secrets.SFTP_DEPLOY_SSH_KEY }}"
    name: id_rsa
    known_hosts: "${{ secrets.SFTP_DEPLOY_KNOWN_HOSTS }}"
    if_key_exists: fail
- name: Deploy via SFTP
  run: echo "mput build" | sftp -b - -rp -P 22324 ec2-user@127.0.0.1:/var/www/build/
```

## Unsupported Options
* DEBUG
* SSH_KEY
  * You will need to place your SSH key in GitHub actions secrets
