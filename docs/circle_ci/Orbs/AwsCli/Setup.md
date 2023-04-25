# CircleCI/AwsCli Setup

## CircleCI Input

```yaml
orbs:
  aws-cli: circleci/aws-cli@x.y
jobs:
  aws-cli-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-cli/setup:
          aws-region: AWS_REGION
          aws-access-key-id: AWS_ACCESS_KEY_ID
          aws-secret-access-key: AWS_SECRET_ACCESS_KEY
```

### Transformed Github Action

```yaml
- uses: aws-actions/configure-aws-credentials@v1
  with:
    aws-region: "${{ env.AWS_REGION }}"
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
```

### Unsupported Options

- configure-default-region
- disable-aws-pager
- override-installed
- profile-name
- role-arn
- version
