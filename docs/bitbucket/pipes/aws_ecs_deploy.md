# Aws Ecs Deploy

[BitBucket Aws Ecs Deploy Documentation](https://bitbucket.org/atlassian/aws-ecs-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/aws-ecs-deploy:1.8.0
variables:
  AWS_ACCESS_KEY_ID: $AWS_ACCESS_KEY_ID
  AWS_SECRET_ACCESS_KEY: $AWS_SECRET_ACCESS_KEY
  AWS_DEFAULT_REGION: 'us-east-1'
  CLUSTER_NAME: 'my-ecs-cluster'
  SERVICE_NAME: 'my-ecs-service'
  TASK_DEFINITION: 'task-definition.json'
```

## Transformed GitHub Action
```yaml
- uses: aws-actions/configure-aws-credentials@v3.0.1
  with:
    aws-access-key-id: "${{ env.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ env.AWS_SECRET_ACCESS_KEY }}"
    aws-region: us-east-1
- id: render-task-definition-my-ecs-service-my-ecs-cluster
  uses: aws-actions/amazon-ecs-render-task-definition@v1.1.3
  with:
    task-definition: task-definition.json
    container-name: UPDATE_ME
    image: UPDATE_ME
- uses: aws-actions/amazon-ecs-deploy-task-definition@v1.4.11
  with:
    task-definition: "${{ steps.render-task-definition-my-ecs-service-my-ecs-cluster.outputs.task-definition }}"
    service: my-ecs-service
    cluster: my-ecs-cluster
    force-new-deployment: 'false'
```

## Unsupported Options
* DEBUG
* WAIT
