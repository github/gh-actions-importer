# Google App Engine Deploy

[BitBucket Google App Engine Deploy Documentation](https://bitbucket.org/atlassian/google-app-engine-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/google-app-engine-deploy:1.3.0
variables:
  KEY_FILE: $KEY_FILE
  PROJECT: 'my-project'
  DEPLOYABLES: 'app-1.yaml app-2.yaml'
  VERSION: 'alpha'
  BUCKET: 'gs://my-bucket'
  IMAGE: 'gcr.io/my/image'
  PROMOTE: 'true'
  STOP_PREVIOUS_VERSION: 'true'
  EXTRA_ARGS: '--logging=debug'
```

## Transformed GitHub Action
```yaml
- uses: actions/checkout@v3.5.0
- uses: google-github-actions/auth@v1.1.1
  with:
    credentials_json: ${{ env.KEY_FILE }}
- uses: google-github-actions/setup-gcloud@v1.1.1
  with:
    project_id: my-project
- run: gcloud app --quiet deploy app-1.yaml app-2.yaml --version=alpha --bucket gs://my-bucket --image-url gcr.io/my/image --logging=debug --promote --stop-previous-version
```

## Unsupported Options
* DEBUG
* CLOUD_BUILD_TIMEOUT
