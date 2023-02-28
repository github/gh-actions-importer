# CircleCI/AwsS3 Setup

## CircleCI input

```yaml
orbs:
  aws-s3: circleci/aws-s3@x.y
jobs:
  aws-s3-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-s3/copy:
          aws-region: AWS_REGION
          aws-access-key-id: AWS_ACCESS_KEY_ID
          aws-secret-access-key: AWS_SECRET_ACCESS_KEY
          to: copy/to
          from: copy/from
          arguments: -sse
```

### Transformed Github Action

```yaml
- uses: aws-actions/configure-aws-credentials@v1
  with:
    aws-region: "${{ env.AWS_REGION }}"
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
- run: aws s3 cp copy/from copy/to -sse
```

### Unsupported Options

- None