# Aws Elasticbeanstalk Deploy

[BitBucket Aws Elasticbeanstalk Deploy Documentation](https://bitbucket.org/atlassian/aws-elasticbeanstalk-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/aws-elasticbeanstalk-deploy
variables:
  AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
  AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
  AWS_DEFAULT_REGION: $AWS_DEFAULT_REGION
  APPLICATION_NAME: "Myawesomeapp"
  ENVIRONMENT_NAME: "Myawesomeapp-env-1"
  ZIP_FILE: "Myawesomeapp.zip"
```

## Transformed GitHub Action
```yaml
- uses: aws-actions/configure-aws-credentials@v2.2.0
  with:
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
    aws-region: "${{ secrets.AWS_DEFAULT_REGION }}"
- name: Upload Package to S3
  run: aws s3 cp Myawesomeapp.zip s3://Myawesomeapp-elasticbeanstalk-deployment
- name: Create new ElasticBeanstalk Application Version
  run: |
    aws elasticbeanstalk create-application-version \
    --application-name "Myawesomeapp" \
    --source-bundle S3Bucket="Myawesomeapp-elasticbeanstalk-deployment",S3Key="Myawesomeapp/Myawesomeapp-${{ github.run_number }}-${{ github.sha }}" \
    --version-label "Myawesomeapp-${{ github.run_number }}-${{ github.sha }}" \
    --description "${{ github.server_url }}/${{ github.repository }}/actions/runs/${{ github.run_id }}"
  if: "${{ 'all' == 'upload-only' || 'all' == 'all' }}"
- name: Deploy new ElasticBeanstalk Application Version
  run: aws elasticbeanstalk update-environment --environment-name Myawesomeapp-env-1 --version-label "Myawesomeapp-${{ github.run_number }}-${{ github.sha }}"
  if: "${{ 'all' == 'deploy-only' || 'all' == 'all' }}"
```

## Unsupported Options
* WAIT
* WAIT_INTERVAL
* WARMUP_INTERVAL
* DEBUG
