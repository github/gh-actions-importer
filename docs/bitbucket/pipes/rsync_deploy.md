# Rsync Deploy

[BitBucket Rsync Deploy Documentation](https://bitbucket.org/atlassian/rsync-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/rsync-deploy:0.9.0
variables:
  USER: 'ec2-user'
  SERVER: '127.0.0.1'
  REMOTE_PATH: '/var/www/build/'
  LOCAL_PATH: 'build'
  DEBUG: 'true'
  SSH_PORT: '8022'
```

## Transformed GitHub Action
```yaml
uses: Burnett01/rsync-deployments@6.0.0
with:
  switches: "-rp --delete"
  remote_host: 127.0.0.1
  remote_user: ec2-user
  remote_path: "/var/www/build/"
  path: build
  remote_port: '8022'
  remote_key: "${{ secrets.SSH_KEY }}"
```

## Unsupported Options
* SSH_KEY
  * The default bitbucket ssh key nor a custom one are migrated. It must be set in your new repository or organization secrets.
* EXTRA_ARGS
* SSH_ARGS
* DELETE_FLAG
  * We add the default switch `--delete`
* DEBUG
