# Aws Code Deploy

[BitBucket Aws Code Deploy Documentation](https://bitbucket.org/atlassian/aws-code-deploy)

## Bitbucket Input

```yaml
# Upload
- pipe: atlassian/aws-code-deploy:1.3.0
  variables:
    AWS_DEFAULT_REGION: 'ap-southeast-2'
    AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
    AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
    COMMAND: 'upload'
    APPLICATION_NAME: 'my-application'
    ZIP_FILE: 'myapp.zip'
# Deploy
- pipe: atlassian/aws-code-deploy:1.3.0
  variables:
    AWS_DEFAULT_REGION: 'ap-southeast-2'
    AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
    AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
    COMMAND: 'deploy'
    APPLICATION_NAME: 'my-application'
    DEPLOYMENT_GROUP: 'my-deployment-group'
```

## Transformed GitHub Action
```yaml
# Upload
- name: Uploading to S3
  run: aws s3 cp myapp.zip s3://my-application-codedeploy-deployment/my-application-${{ github.run_number }}-${{ github.sha }}
- name: Creating new CodeDeploy Application Revision
  run: |
    aws deploy register-application-revision \
    --application-name my-application \
    --revision revisionType=s3,s3Location={bucket=my-application-codedeploy-deployment,key=my-application-codedeploy-deployment/my-application-${{ github.run_number }}-${{ github.sha }},bundleType=zip}"

# Deploy
- name: Check revision exists
  run: aws deploy get-application-revision --application-name my-application --revision revisionType=s3,s3Location={bucket=my-application-codedeploy-deployment,key=my-application-codedeploy-deployment/my-application-${{ github.run_number }}-${{ github.sha }},bundleType=zip}
- name: Deploy app from revision
  run: |
    aws deploy create-deployment \
    --application-name my-application \
    --deployment-group-name my-deployment-group \
    --revision revisionType=s3,s3Location={bucket=my-application-codedeploy-deployment,key=my-application-codedeploy-deployment/my-application-${{ github.run_number }}-${{ github.sha }},bundleType=zip} \
    --ignore-application-stop-failures false
```

## Unsupported Options
* WAIT
* WAIT_INTERVAL
* DEBUG
