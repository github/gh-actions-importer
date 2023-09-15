# Aws Sam Deploy

[BitBucket Aws Sam Deploy Documentation](https://bitbucket.org/atlassian/aws-sam-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/aws-sam-deploy:2.1.0
  variables:
    AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
    AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
    COMMAND: 'deploy'
    DEBUG: 'true'
```

## Transformed GitHub Action

```yaml
- uses: aws-actions/setup-sam@v2
- uses: aws-actions/configure-aws-credentials@v3.0.1
  with:
    aws-access-key-id: "${{ env.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ env.AWS_SECRET_ACCESS_KEY }}"
- run: sam deploy --no-confirm-changeset --no-fail-on-empty-changeset --debug
```

## Unsupported Options

