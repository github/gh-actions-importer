# CircleCI/AwsEcr Push Image

## CircleCI Input

```yaml
orbs:
  aws-ecr: circleci/aws-ecr@x.y
jobs:
  aws-ecr-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecr/push-image:
          account-url: AWS_ECR_ACCOUNT_URL
          repo: my-repo
          tags: my-tag-1, my-tag-2
```

### Transformed Github Action

```yaml
- run: |-
    docker push ${{ env.AWS_ECR_ACCOUNT_URL }}/my-repo:my-tag-1
    docker push ${{ env.AWS_ECR_ACCOUNT_URL }}/my-repo:my-tag-2
```

### Unsupported Options

- None
