# Job Mappings

Click [here](https://docs.travis-ci.com/user/job-lifecycle/) to view the full listing of TravisCI Jobs.

| TravisCI                                                            | GitHub                                        |
| :------------------------------------------------------------------ | :-------------------------------------------- |
| [AfterFailure](AfterFailure.md)                                     | run, if                                       |
| [AfterSuccess](AfterSuccess.md)                                     | run, if                                       |
| [Branches](Branches.md)                                             | on                                            |
| [BuildPullRequests](BuildPullRequests.md)                           | on                                            |
| [Cache](Cache.md)                                                   | actions/cache@v2                              |
| [Conditions](Condtiions.md)                                         | if                                            |
| [Dependencies](Dependencies.md)                                     | ruby/setup-ruby@v1.138.0, actions/setup-java@v3.10.0     |
| [Git](Git.md)                                                       | actions/checkout@v2                           |
| [Notifications](Notifications.md)                                   | rectalogic/notify-irc@v1                      |
| [OsxImage](OsxImage.md)                                             | maxim-lobanov/setup-xcode@v1                  |
| [Deploy](Deploy.md)                                                 | run                                           |
| [Scripts](Scripts.md)                                               | run                                           |
| [Services](Services.md)                                             | services                                      |

Any jobs not listed above will not be mapped to an action and will be left as a comment in the converted workflow.

## Unsupported

The following jobs do not have any equivalent in GitHub Actions:

- Sudo
- Compiler
- Version
- MaximumNumberOfBuilds
- Config Validation
