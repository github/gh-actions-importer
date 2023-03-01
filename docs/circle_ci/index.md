# Concept Mappings

| CircleCI                                          | GitHub                       |
| :------------------------------------------------ | :--------------------------- |
| [AddSshKeys](Steps/AddSshKeys.md)                 | run                          |
| [AttachWorkspace](Steps/AttachWorkspace.md)       | actions/download-artifact@v2 |
| [Checkout](Steps/Checkout.md)                     | actions/checkout@v2          |
| [Deploy](Executors/Deploy.md)                     | run                          |
| [Docker](Executors/Docker.md)                     | container                    |
| [Machine](Executors/Machine.md)                   | runs-on                      |
| [Macos](Executors/Macos.md)                       | runs-on                      |
| [PersistToWorkspace](Steps/PersistToWorkspace.md) | actions/upload-artifact@v2   |
| [Run](Steps/Run.md)                               | run                          |
| [Schedule](Triggers/Schedule.md)                  | on                           |
| [Store Test Results](Steps/StoreTestResults.md)   | actions/upload-artifact@v2   |
| [Store Artifacts](Steps/StoreArtifacts.md)        | actions/upload-artifact@v2   |
| [Unless](Steps/Unless.md)                         | if                           |
| [When](Steps/When.md)                             | if                           |

Any jobs not listed above will not be mapped to an action and will be left as a comment in the converted workflow.

## Unsupported

The following steps do not have any equivalent in GitHub Actions:

- setup_remote_docker

The following job properties do not have any equivalent in GitHub Actions:

- resource_class
- parallelism
- branches (deprecated in CircleCI)
- circleci_ip_ranges

The following concepts are not supported by GitHub Actions Importer:

- executor type parameters
- dynamic configuration pipelines (`setup` key)

## Orb Mappings

| CircleCI                                       | GitHub                                                  |
| :--------------------------------------------- | :------------------------------------------------------ |
| [circleci/aws-cli](Orbs/AwsCli)                | aws-actions/configure-aws-credentials@v1                |
| [circleci/aws-ecr](Orbs/AwsEcr)                | aws-actions/amazon-ecr-login@v1.5.3                     |
| [circleci/aws-ecs](Orbs/AwsEcs)                | aws-actions/amazon-ecs-render-task-definition@v1.1.2    |
| [circleci/aws-s3](Orbs/AwsS3)                  | aws-actions/configure-aws-credentials@v1, run           |
| [circleci/browser-tools](Orbs/BrowserTools)    | -                                                       |
| [cypress-io/cypress](Orbs/Cypress)             | cypress-io/github-action@v2, run                        |
| [circleci/docker](Orbs/Docker)                 | hadolint/hadolint-action@v1.6.0, run                    |
| [circleci/go](Orbs/Go)                         | actions/setup-go@v3, actions/cache@v3, run              |
| [circleci/heroku](Orbs/Heroku)                 | run                                                     |
| [circleci/node](Orbs/Node)                     | actions/setup-node@v2, actions/cache@v2, run            |
| [circleci/python](Orbs/Python)                 | actions/cache@v2, run                                   |
| [circleci/ruby](Orbs/Ruby)                     | ruby/setup-ruby@v1.138.0, run                           |
| [circleci/slack](Orbs/Slack)                   | rtCamp/action-slack-notify@v2.2.0                       |
| [circleci/windows](Orbs/Windows)               | runs-on                                                 |
