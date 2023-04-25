# CircleCI/AwsEcr Ecr Login

## CircleCI Input

```yaml
orbs:
  aws-ecr: circleci/aws-ecr@x.y
jobs:
  aws-ecr-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecr/ecr-login:
          region: AWS_REGION
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
- uses: aws-actions/amazon-ecr-login@v1.5.3
```

### Unsupported Options

- account-url
- profile-name
