# CircleCI/Aws-Ecs RunTask

## CircleCI Input

```yaml
orbs:
  aws-ecs: circleci/aws-ecs@x.y
jobs:
  aws-ecs-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecs/run-task:
          task-definition: my-task-def.json

```

### Transformed Github Action

```yaml
- uses: aws-actions/configure-aws-credentials@v1
  with:
    aws-region: "${{ env.AWS_DEFAULT_REGION }}"
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
  run: aws ecs run-task --task-definition my-task-def.json
```

### Unsupported Options

- docker-image-for-job
