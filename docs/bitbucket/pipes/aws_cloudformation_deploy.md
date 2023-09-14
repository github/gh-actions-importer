# Aws Cloudformation Deploy

[BitBucket Aws Cloudformation Deploy Documentation](https://bitbucket.org/atlassian/aws-cloudformation-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/aws-cloudformation-deploy:0.16.0
variables:
  AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
  AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
  AWS_DEFAULT_REGION: 'us-east-1'
  STACK_NAME: 'my-stack-name'
  TEMPLATE: 'https://s3.amazonaws.com/cfn-deploy-pipe/cfn-template.json'
  CAPABILITIES: ['CAPABILITY_IAM', 'CAPABILITY_AUTO_EXPAND']
  STACK_PARAMETERS: >
    [{
      "ParameterKey": "KeyName",
      "ParameterValue": "mykey"
    },
    {
      "ParameterKey": "DBUser",
      "ParameterValue": "mydbuser"
    },
    {
      "ParameterKey": "DBPassword",
      "ParameterValue": $DB_PASSWORD
    }]
  TAGS: >
    [{
      "Key": "Environment",
      "Value": "TEST"
    },
    {
      "Key": "Application",
      "Value": "myApp"
    }]
```

## Transformed GitHub Action
```yaml
- uses: aws-actions/configure-aws-credentials@v2.2.0
  with:
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
    aws-region: "${{ secrets.AWS_DEFAULT_REGION }}"
- uses: aws-actions/aws-cloudformation-github-deploy@v1.2.0
  with:
    name: my-stack-name
    template: https://s3.amazonaws.com/cfn-deploy-pipe/cfn-template.json
    capabilities: CAPABILITY_IAM,CAPABILITY_AUTO_EXPAND
    parameter-overrides: KeyName=mykey,DBUser=mydbuser,DBPassword=$DB_PASSWORD
    tags: Environment=TEST,Application=myApp
```

## Unsupported Options
* WAIT
* WAIT_INTERVAL
* WITH_DEFAULT_TAGS
* OUTPUTS_FILENAME
  * The resulting [GitHub Action has outputs](https://github.com/aws-actions/aws-cloudformation-github-deploy/blob/b93bbf71c654d3036f1a988d20eeb9a2d7886ad6/action.yml#L55-L57) that can be used in place of this option.
* EXTRA_PARAMS
  * It's possible that [some of these inputs are supported natively](https://github.com/aws-actions/aws-cloudformation-github-deploy/blob/master/action.yml) by the resulting action.
* DEBUG

