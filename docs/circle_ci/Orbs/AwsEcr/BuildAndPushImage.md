# CircleCI/AwsEcr Build And Push Image

## CircleCI Input

```yaml
orbs:
  aws-ecr: circleci/aws-ecr@x.y
jobs:
  aws-ecr-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecr/build-and-push-image:          
          account-url: AWS_ECR_ACCOUNT_URL
          attach-workspace: true
          aws-access-key-id: AWS_ACCESS_KEY_ID
          aws-secret-access-key: AWS_SECRET_ACCESS_KEY
          checkout: true
          create-repo: true
          docker-login: true
          dockerfile: MyDockerFile
          dockerhub-username: DOCKERHUB_USERNAME
          dockerhub-password: DOCKERHUB_PASSWORD
          extra-build-args: -e example
          no-output-timeout: "20m"
          path: .
          region: AWS_REGION
          repo: my-repo
          tag: my-tag-1, my-tag-2
          workspace-root: my-path
```

### Transformed Github Action

```yaml
- uses: actions/checkout@v2
- uses: actions/download-artifact@v2
  with:
    path: my/path
- uses: aws-actions/configure-aws-credentials@v1
  with:
    aws-region: "${{ env.AWS_REGION }}"
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
- uses: aws-actions/amazon-ecr-login@v1.5.3
- run: aws ecr create-repository --profile non-default --region ${{ env.AWS_REGION }} --repository-name my-repo --image-scanning-configuration scanOnPush=true
- run: docker login -u ${{ env.MY_DOCKERHUB_USER }} -p ${{ env.MY_DOCKERHUB_PASS }}
- run: |-
    docker build -e example -f ./MyDockerFile -t ${{ env.AWS_ECR_ACCOUNT_URL }}/my-repo:my-tag-1 .
    docker build -e example -f ./MyDockerFile -t ${{ env.AWS_ECR_ACCOUNT_URL }}/my-repo:my-tag-2 .
  timeout-minutes: '20'
- run: docker push ${{ env.AWS_EXAMPLE_ENV_VAR }}/my-repo:my-tag
```

### Unsupported Options

- setup-remote-docker
- remote-docker-layer-caching
- remote-docker-version
- skip-when-tags-exist
- repo-scan-on-push
- profile-name
