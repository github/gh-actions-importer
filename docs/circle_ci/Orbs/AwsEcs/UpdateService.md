# CircleCI/Aws-Ecs Update Service

## CircleCI Input

```yaml
orbs:
  aws-ecs: circleci/aws-ecs@x.y
jobs:
  aws-ecs-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - aws-ecs/update-service:
        task-definition: my-task-def.json,
        cluster-name: my-cluster-name, 
        family: my-ecs-family, 
        deployment-controller: CODE_DEPLOY, 
        codedeploy-application-name: my-app-name,
        codedeploy-deployment-group-name: my-deployment-group, 
        container-env-var-updates: container=container-name,name=env-var-name,value=env-var-value,
        force-new-deployment: true, 
        service-name: my-service, 
        verification-timeout: 20m
```

### Transformed Github Action

```yaml
- uses: aws-actions/amazon-ecs-render-task-definition@v1.1.2
  with:
    task-definition: "{{ env.TASK_DEFINITION_JSON }}"
    container-name: "${{ env.ECS_CONTAINER_NAME }}"
    image: "${{ env.ECS_CONTAINER_IMAGE_URI }}"
- uses: aws-actions/amazon-ecs-deploy-task-definition@v1.4.11
  with:
    task-definition: "{{ env.TASK_DEFINITION_JSON }}"
    service: my-service
    cluster: my-cluster-name
    wait-for-service-stability: true
    codedeploy-appspec: "${{ env.CODE_DEPLOY_APPSPEC }}"
    codedeploy-application: my-app-name
    codedeploy-deployment-group: my-deployment-group
    wait-for-minutes: '20'
    force-new-deployment: true
  env:
    CONTAINER: container-name
    NAME: env-var-name
    VALUE: env-var-value
```

### Unsupported Options

- skip-task-definition-registration
- task-definition-tag
- codedeploy-load-balanced-container-name
- codedeploy-load-balanced-container-port
- container-image-name-updates
- max-poll-attempts
- poll-interval
- fail-on-verification-timeout
