# CircleCI/AwsEcr Build Image

## CircleCI Input

```yaml
orbs:
  aws-ecr: circleci/aws-ecr@x.y
jobs:
  aws-ecr-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecr/build-image:
          region: AWS_REGION
          aws-access-key-id: AWS_ACCESS_KEY_ID
          aws-secret-access-key: AWS_SECRET_ACCESS_KEY
          checkout: true
          ecr-login: true
          account-url: AWS_ECR_ACCOUNT_URL
          dockerfile: MyDockerFile
          path: .
          extra-build-args: -e example
          repo: my-repo
          tags: my-tag-1, my-tag-2
          no-output-timeout: "20m"
```

### Transformed Github Action

```yaml
- uses: aws-actions/configure-aws-credentials@v1
  with:
    aws-region: "${{ env.AWS_REGION }}"
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
- uses: aws-actions/amazon-ecr-login@v1.5.3
- run: |-
    docker build -e example -f ./MyDockerFile -t ${{ env.AWS_ECR_ACCOUNT_URL }}/my-repo:my-tag-1 .
    docker build -e example -f ./MyDockerFile -t ${{ env.AWS_ECR_ACCOUNT_URL }}/my-repo:my-tag-2 .
  timeout-minutes: '20'
```

### Unsupported Options

- skip-when-tags-exist
- profile-name
