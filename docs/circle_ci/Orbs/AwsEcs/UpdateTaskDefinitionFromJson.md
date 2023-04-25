# CircleCI/Aws-Ecs UpdateTaskDefinitionFromJson

## CircleCI Input

```yaml
orbs:
  aws-ecs: circleci/aws-ecs@x.y
jobs:
  aws-ecs-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecs/update-task-definition-from-json:
          task-definition-json: my-task-def.json
```

### Transformed Github Action

```yaml
- uses: aws-actions/amazon-ecs-render-task-definition@v1.1.2
  with:
    task-definition: my-task-def.json
    container-name: ${{ env.ECS_CONTAINER_NAME }}
    image: ${{ env.ECS_CONTAINER_IMAGE_URI }}
```

### Unsupported Options

- None
