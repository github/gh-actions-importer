# Artifacts

## Travis Input

```yaml
artifacts:
  enabled: true
  bucket: bucket
  endpoint: string
  paths: my/path
```

### Transformed Github Action

```yaml
- uses: aws-actions/configure-aws-credentials@v1
  with:
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
  if: true
- id: upload_artifacts_to_s3_bucket
  run: aws s3 cp my/path s3://bucket/ --recursive
  if: true
```

### Unsupported Options

- endpoint
- key
- secret
- branch
- log_format
- debug
- concurrency
- max_size
- permissions
- working_dir
- cache_control
