# CircleCI/AwsEcr Create Repo

## CircleCI Input

```yaml
orbs:
  aws-ecr: circleci/aws-cli@x.y
jobs:
  aws-ecr-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecr/create-repo:
          region: AWS_REGION
          repo: my-repo-name
          profile-name: my-profile
          repo-scan-on-push: true
```

### Transformed Github Action

```yaml
- run: aws ecr create-repository --profile my-profile --region ${{ env.AWS_REGION }} --repository-name my-repo-name --image-scanning-configuration scanOnPush=true
```

### Unsupported Options

- None
