# Google Cloud Storage Deploy

[BitBucket Google Cloud Storage Deploy Documentation](https://bitbucket.org/atlassian/google-cloud-storage-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/google-cloud-storage-deploy:1.3.0
variables:
  KEY_FILE: $KEY_FILE
  PROJECT: 'my-project'
  BUCKET: 'my-bucket'
  SOURCE: '.'
  CACHE_CONTROL: 'max-age=30'
  CONTENT_DISPOSITION: 'attachment'
  ACL: 'public-read'
  STORAGE_CLASS: 'nearline'
  EXTRA_ARGS: '-e'
```

## Transformed GitHub Action
```yaml
- uses: google-github-actions/auth@v1.1.1
  with:
    credentials_json: ${{ env.KEY_FILE }}
- uses: google-github-actions/setup-gcloud@v1.1.1
  with:
    project_id: my-project
- uses: google-github-actions/upload-cloud-storage@v1.0.3
  with:
    path: "."
    destination: my-bucket
    project_id: my-project
    headers: |-
      cache-control: max-age=30
      content-disposition: attachment
    predefinedAcl: public-read
```

## Unsupported Options
* DEBUG
* STORAGE_CLASS
* EXTRA_ARGS
