# Pipe Mappings

| Bitbucket Pipelines                                                           | GitHub                                                                                                                              |
| :----------------------------------------------------------------             | :---------------------------------------------------------------------------------------------------------------------------------- |
| [aws-cloudformation-deploy](pipes/aws_cloudformation_deploy.md)               | aws-actions/aws-cloudformation-github-deploy                                                                                        |
| [aws-cloudfront-invalidate](pipes/aws_cloudfront_invalidate.md)               | aws-actions/configure-aws-credentials, run                                                                                          |
| [aws-code-deploy](pipes/aws_code_deploy.md)                                   | aws-actions/configure-aws-credentials, run                                                                                          |
| [aws-ecs-deploy](pipes/aws_ecs_deploy.md)                                     | aws-actions/configure-aws-credentials, aws-actions/amazon-ecs-deploy-task-definition, aws-actions/amazon-ecs-render-task-definition |
| [aws-elasticbeanstalk-deploy](pipes/aws_elasticbeanstalk_deploy.md)           | aws-actions/configure-aws-credentials, run                                                                                          |
| [aws-lambda-deploy](pipes/aws_lambda_deploy.md)                               | aws-actions/configure-aws-credentials, run                                                                                          |
| [aws-s3-deploy](pipes/aws_s3_deploy.md)                                       | aws-actions/configure-aws-credentials, run                                                                                          |
| [aws-sam-deploy](pipes/aws_sam_deploy.md)                                     | aws-actions/configure-aws-credentials, aws-actions/setup-sam, run                                                                   |
| [azure-aca-deploy](pipes/azure_aca_deploy.md)                                 | azure/login, azure/container-apps-deploy-action                                                                                     |
| [azure-acr-push-image](pipes/azure_acr_push_image.md)                         | azure/docker-login, run                                                                                                             |
| [azure-arm-deploy](pipes/azure_arm_deploy.md)                                 | azure/login, azure/cli, azure/arm-deploy                                                                                            |
| [azure-cli-run](pipes/azure_cli_run.md)                                       | azure/login, azure/cli                                                                                                              |
| [azure-functions-deploy](pipes/azure_functions_deploy.md)                     | azure/login, azure/functions-action                                                                                                 |
| [azure-storage-deploy](pipes/azure_storage_deploy.md)                         | azure/login, run                                                                                                                    |
| [azure-web-apps-containers-deploy](pipes/azure_web_apps_containers_deploy.md) | azure/login, run                                                                                                                    |
| [azure-web-apps-deploy](pipes/azure_web_apps_deploy.md)                       | azure/login, azure/webapps-deploy                                                                                                   |
| [bitbucket-upload-file](pipes/bitbucket_upload_file.md)                       | actions/upload-artifact                                                                                                             |
| [checkstyle-report](pipes/checkstyle_report.md)                               | jwgmeligmeyling/checkstyle-github-action                                                                                            |
| [firebase-deploy](pipes/firebase_deploy.md)                                   | FirebaseExtended/action-hosting-deploy                                                                                              |
| [ftp-deploy](pipes/ftp_deploy.md)                                             | run                                                                                                                                 |
| [git-secrets-scan](pipes/git_secrets_scan.md)                                 | actions/checkout, run                                                                                                               |
| [google-app-engine-deploy](pipes/google_app_engine_deploy.md)                 | google-github-actions/auth, google-github-actions/setup-gcloud, run                                                                 |
| [google-cloud-storage-deploy](pipes/google_cloud_storage_deploy.md)           | google-github-actions/auth, google-github-actions/upload-cloud-storage                                                              |
| [google-gke-kubectl-run](pipes/google_gke_kubectl_run.md)                     | google-github-actions/auth, google-github-actions/setup-gcloud, run                                                                 |
| [heroku-deploy](pipes/heroku_deploy.md)                                       | AkhileshNS/heroku-deploy                                                                                                            |
| [npm-publish](pipes/npm_publish.md)                                           | actions/setup-node, run                                                                                                             |
| [pypi-publish](pipes/pypi_publish.md)                                         | actions/setup-python, pypa/gh-action-pypi-publish, run                                                                              |
| [rsync-deploy](pipes/rsync_deploy.md)                                         | Burnett01/rsync-deployments                                                                                                         |
| [serverless-deploy](pipes/serverless_deploy.md)                               | serverless/github-action, run                                                                                                       |
| [sftp-deploy](pipes/sftp_deploy.md)                                           | shimataro/ssh-key-action, run                                                                                                       |
| [slack-notify](pipes/slack_notify.md)                                         | slackapi/slack-github-action                                                                                                        |
| [trigger-pipeline](pipes/trigger_pipeline.md)                                 | run                                                                                                                                 |



### Unsupported

Any pipe not listed above will not be mapped to an action and will be left as a comment in the converted workflow.
