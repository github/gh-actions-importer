# Task Mappings

| Bamboo CI                                        | GitHub                                                            |
| :----------------------------------------------- | :---------------------------------------------------------------- |
| [Ant](plugins/Ant.md)                            | actions/setup-java, run                                           |
| [Artifact Download](plugins/ArtifactDownload.md) | actions/download-artifact                                         |
| [AWS Code Deploy](plugins/AWSCodeDeploy.md)      | aws-actions/configure-aws-credentials, run                        |
| [Bower](plugins/Bower.md)                        | actions/setup-node, run                                           |
| [Checkout](plugins/Checkout.md)                  | actions/checkout                                                  |
| [Clean](plugins/Clean.md)                        | run                                                               |
| [Command](plugins/Command.md)                    | run                                                               |
| [Docker CLI](plugins/Dockercli.md)               | run                                                               |
| [Fastlane](plugins/Fastlane.md)                  | ruby/setup-ruby, run                                              |
| [Grails](plugins/Grails.md)                      | actions/setup-java, run, EnricoMi/publish-unit-test-result-action |
| [Grunt](plugins/Grunt.md)                        | actions/setup-node, run                                           |
| [Gulp](plugins/Gulp.md)                          | actions/setup-node, run                                           |
| [Inject Variables](plugins/InjectVariables.md)   | run                                                               |
| [Junit](plugins/Junit.md)                        | EnricoMi/publish-unit-test-result-action                          |
| [Maven](plugins/Maven.md)                        | actions/setup-java                                                |
| [Mocha](plugins/Mocha.md)                        | EnricoMi/publish-unit-test-result-action                          |
| [Ms Build](plugins/MsBuild.md)                   | run                                                               |
| [MS Test](plugins/MsTest.md)                     | EnricoMi/publish-unit-test-result-action                          |
| [Node](plugins/Node.md)                          | run                                                               |
| [Npm](plugins/Npm.md)                            | actions/setup-node, run                                           |
| [NUnit Parser](plugins/NunitParser.md)           | EnricoMi/publish-unit-test-result-action                          |
| [NUnit Runner](plugins/NUnitRunner.md)           | microsoft/vstest-action, EnricoMi/publish-unit-test-result-action |
| [Repository Branch](plugins/RepositoryBranch.md) | run                                                               |
| [Repository Commit](plugins/RepositoryCommit.md) | run                                                               |
| [Repository Push](plugins/RepositoryPush.md)     | run                                                               |
| [Repository Tag](plugins/RepositoryTag.md)       | run                                                               |
| [Scp](plugins/Scp.md)                            | appleboy/scp-action                                               |
| [Ssh](plugins/Ssh.md)                            | shimataro/ssh-key-action, run                                     |
| [Stop Job](plugins/StopJob.md)                   | run                                                               |
| [TestNG](plugins/TestNG.md)                      | scacap/action-surefire-report                                     |
| [Unlock Keychain](plugins/UnlockKeychain.md)     | run                                                               |
| [Visual Studio](plugins/VisualStudio.md)        | seanmiddleditch/gha-setup-vsdevenv, run                           |

## Trigger Mappings

| Bamboo CI                                                          | GitHub                                        |
| :----------------------------------------------------------------- | :-------------------------------------------- |
| [Bitbucket Cloud Trigger](triggers/BitbucketCloudTrigger.md)       | repository_dispatch                           |
| [Build Success](triggers/BuildSuccess.md)                          | workflow_dispatch                             |
| [Cron](triggers/Cron.md)                                           | schedule                                      |
| [Environment Success](triggers/EnvironmentSuccess.md)              | workflow_dispatch                             |
| [Remote](triggers/Remote.md)                                       | push                                          |
| [Repository Polling Trigger](triggers/RepositoryPollingTrigger.md) | schedule                                      |
| [Stage Success](triggers/StageSuccess.md)                          | workflow_dispatch                             |
| [Tag](triggers/Tag.md)                                             | push                                          |

### Unsupported

- [Single daily build](triggers/SingleDailyBuild.md)

Any tasks or triggers not listed above will not be mapped to an action and will be left as a comment in the converted workflow.
