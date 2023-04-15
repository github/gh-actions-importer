# [CircleCI AwsEcr](https://circleci.com/developer/orbs/orb/circleci/aws-ecr)

| CircleCI                                                            | GitHub                                                                                                                            |
| :------------------------------------------------------------------ | :-------------------------------------------------------------------------------------------------------------------------------- |
| [BuildAndPushImage](BuildAndPushImage.md)                           | actions/checkout@v1, actions/download-artifact@v2, aws-actions/configure-aws-credentials@v1, aws-actions/amazon-ecr-login, run    |
| [BuildImage](BuildImage.md)                                         | aws-actions/configure-aws-credentials@v1, aws-actions/amazon-ecr-login, run                                                       |
| [CreateRepo](CreateRepo.md)                                         | run                                                                                                                               |
| [Default](Default.md)                                               | runs-on                                                                                                                           |
| [EcrLogin](EcrLogin.md)                                             | aws-actions/configure-aws-credentials@v1, aws-actions/amazon-ecr-login                                                            |
| [PushImage](PushImage.md)                                           | run                                                                                                                               |

## Indeterminate behavior on self-hosted runners

- create-repo
- ecr-login
