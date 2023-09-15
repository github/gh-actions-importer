# Aws S3 Deploy

[BitBucket Aws S3 Deploy Documentation](https://bitbucket.org/atlassian/aws-s3-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/aws-s3-deploy:1.2.0
variables:
  AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
  AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
  AWS_DEFAULT_REGION: $AWS_DEFAULT_REGION
  CACHE_CONTROL: 'max-age=31536000'
  CONTENT_ENCODING: 'gzip'
  ACL: 'public-read'
  EXPIRES: '2021-12-31T23:59:59Z'
  STORAGE_CLASS: 'STANDARD'
  DELETE_FLAG: 'true'
  EXTRA_ARGS: '--dryrun --only-show-errors'
  DEBUG: 'true'
```

## Transformed GitHub Action
```yaml
- uses: aws-actions/configure-aws-credentials@v2.2.0
  with:
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
    aws-region: "${{ secrets.AWS_DEFAULT_REGION }}"
- run: aws s3 sync s3://${{ secrets.S3_BUCKET }} --cache-control max-age=31536000 --content-encoding gzip --acl public-read --expires 2021-12-31T23:59:59Z --storage-class STANDARD --delete --dryrun --only-show-errors
```


## Unsupported Options
* DEBUG
* PRE_EXECUTION_SCRIPT
