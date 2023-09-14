# Aws Cloudfront Invalidate

[BitBucket Aws Cloudfront Invalidate Documentation](https://bitbucket.org/atlassian/aws-cloudfront-invalidate)

## Bitbucket Input

```yaml
- pipe: atlassian/aws-cloudfront-invalidate:0.7.0
  variables:
  AWS_ACCESS_KEY_ID: my_access_key
  AWS_SECRET_ACCESS_KEY:  $AWS_SECRET_SERVERLESS_DEPLOY_KEY
  AWS_DEFAULT_REGION: us-east-1
  DISTRIBUTION_ID: dist_id
  PATHS: index.html fun/*
```

## Transformed GitHub Action
```yaml
    - name: Invalidate Cloudfront Distribution
      run: aws cloudfront create-invalidation --distribution-id dist_id --paths index.html "fun/*"
```

## Unsupported Options
- DEBUG
