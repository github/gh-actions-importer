# Aws Lambda Deploy

[BitBucket Aws Lambda Deploy Documentation](https://bitbucket.org/atlassian/aws-lambda-deploy)

## Bitbucket Input

```yaml
- pipe: atlassian/aws-lambda-deploy:1.9.0
  variables:
    AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
    AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
    AWS_DEFAULT_REGION: $AWS_DEFAULT_REGION
    FUNCTION_NAME: 'my-lambda-function'
    COMMAND: 'update'
    ZIP_FILE: 'my-lambda.zip'
    WAIT: 'true'
```

## Transformed GitHub Action
```yaml
- uses: aws-actions/configure-aws-credentials@v3.0.1
  with:
    aws-access-key-id: "$AWS_ACCESS_KEY_ID"
    aws-secret-access-key: "$AWS_SECRET_ACCESS_KEY"
    aws-region: "$AWS_DEFAULT_REGION"
- name: Update Lambda Function Code
  run: aws lambda update-function-code --function-name my-lambda-function --zip-file fileb://my-lambda.zip --publish
```

## Unsupported Options
- WAIT
- WAIT_INTERVAL
- DEBUG
