# Trigger Pipeline

[BitBucket Trigger Pipeline Documentation](https://bitbucket.org/atlassian/trigger-pipeline)

## Bitbucket Input

```yaml
pipe: atlassian/trigger-pipeline:5.2.0
variables:
  BITBUCKET_USERNAME: $BITBUCKET_USERNAME
  BITBUCKET_APP_PASSWORD: $BITBUCKET_APP_PASSWORD
  REPOSITORY: 'your-awesome-repo'
  REF_TYPE: 'branch'
  REF_NAME: 'master'
  CUSTOM_PIPELINE_NAME: 'deployment-pipeline'
  PIPELINE_VARIABLES: >
      [{
        "key": "AWS_DEFAULT_REGION",
        "value": "us-west-1"
      },
      {
        "key": "AWS_ACCESS_KEY_ID",
        "value": "$AWS_ACCESS_KEY_ID",
        "secured": true
      },
      {
        "key": "AWS_SECRET_ACCESS_KEY",
        "value": "$AWS_SECRET_ACCESS_KEY",
        "secured": true
      }]
  WAIT: 'true'
```

## Transformed GitHub Action
```yaml
name: Trigger GitHub Workflow
run: |-
  gh workflow run TARGET_WORKFLOW_NAME.yml --repo ${{ github.repository_owner }}/your-awesome-repo --ref master \
    -f AWS_DEFAULT_REGION='us-west-1' \
    -f AWS_ACCESS_KEY_ID='$AWS_ACCESS_KEY_ID' \
    -f AWS_SECRET_ACCESS_KEY='$AWS_SECRET_ACCESS_KEY'
env:
  GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
```

## Unsupported Options
* BITBUCKET_USERNAME
* BITBUCKET_APP_PASSWORD
* BITBUCKET_ACCESS_TOKEN
* CUSTOM_PIPELINE_NAME
  * This will not map 1:1 from BitBucket, you will need to identify the correct GitHub workflow name to target after migration
* REF_TYPE
  * In GitHub tags and branches are treated the same
* WAIT
* WAIT_MAX_TIMEOUT
* DEBUG
