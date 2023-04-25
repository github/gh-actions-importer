# GitLab

## Designer Pipeline

### Jenkins Input

```xml
<com.dabsquared.gitlabjenkins.GitLabPushTrigger plugin="gitlab-plugin@1.5.13">
    <spec/>
    <triggerOnPush>true</triggerOnPush>
    <triggerOnMergeRequest>true</triggerOnMergeRequest>
    <triggerOnPipelineEvent>false</triggerOnPipelineEvent>
    <triggerOnAcceptedMergeRequest>false</triggerOnAcceptedMergeRequest>
    <triggerOnClosedMergeRequest>false</triggerOnClosedMergeRequest>
    <triggerOnApprovedMergeRequest>false</triggerOnApprovedMergeRequest>
    <triggerOpenMergeRequestOnPush>never</triggerOpenMergeRequestOnPush>
    <triggerOnNoteRequest>true</triggerOnNoteRequest>
    <noteRegex>Jenkins please retry a build</noteRegex>
    <ciSkip>true</ciSkip>
    <skipWorkInProgressMergeRequest>true</skipWorkInProgressMergeRequest>
    <setBuildDescription>true</setBuildDescription>
    <branchFilterType>NameBasedFilter</branchFilterType>
    <includeBranchesSpec>main, dev</includeBranchesSpec>
    <excludeBranchesSpec/>
    <sourceBranchRegex/>
    <targetBranchRegex/>
    <secretToken>{AQAAABAAAAAQvxz/08QYKCD0/NqNcMbCyQSU+EG4gjPVLomKmvOzFyI=}</secretToken>
    <pendingBuildName/>
    <cancelPendingBuildsOnUpdate>false</cancelPendingBuildsOnUpdate>
</com.dabsquared.gitlabjenkins.GitLabPushTrigger>
</triggers>
<publishers>
<com.dabsquared.gitlabjenkins.publisher.GitLabAcceptMergeRequestPublisher plugin="gitlab-plugin@1.5.13"/>
</publishers>
```
### Transformed Github Action

```yaml
on:
  push:
    branches:
    - main
    - dev
  pull_request:
    branches:
    - main
    - dev
jobs:
  build:
    runs-on:
      - ubuntu-latest
    steps:
    - name: checkout
      uses: actions/checkout@v2
#     # 'com.dabsquared.gitlabjenkins.publisher.GitLabAcceptMergeRequestPublisher' was not transformed because there is no suitable equivalent in GitHub Actions
```

### Unsupported Options
- Rebuild open Merge Requests
- Approved Merge Requests (EE-only)
- Comments
- Comment (regex) for triggering a build
- Enable ci-skip
- Ignore WIP Merge Requests
- Set build description to build cause (eg. Merge request or Git Push)
- Build on successful pipeline events
- Pending build name for pipeline
- Cancel pending merge request builds on update
- Filter branches by regex
- Filter merge request by label

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
pipeline {
    agent any
    post {
      failure {
        updateGitlabCommitStatus name: 'build', state: 'failed'
      }
      success {
        updateGitlabCommitStatus name: 'build', state: 'success'
      }
    }
    options {
      gitLabConnection('your-gitlab-connection-name')
    }
    triggers {
        gitlab(
            triggerOnPush: true,
            triggerOnMergeRequest: true, triggerOpenMergeRequestOnPush: "never",
            triggerOnNoteRequest: false,
            noteRegex: "Jenkins please retry a build",
            skipWorkInProgressMergeRequest: true,
            ciSkip: false,
            setBuildDescription: true,
            addNoteOnMergeRequest: true,
            addCiMessage: true,
            addVoteOnMergeRequest: true,
            acceptMergeRequestOnSuccess: false,
            branchFilterType: "All",
            includeBranchesSpec: "release/qat",
            excludeBranchesSpec: "",
            pendingBuildName: "Jenkins",
            cancelPendingBuildsOnUpdate: false,
            secretToken: "abcdefghijklmnopqrstuvwxyz0123456789ABCDEF")
        }
    stages {
      stage("build") {
        steps {
          echo "hello world"
        }
      }
    }
}
```

### Transformed Github Action

```yaml
on:
  push:
  pull_request:
jobs:
  build:
```

### Unsupported Options
- triggerOnNoteRequest
- noteRegex
- skipWorkInProgressMergeRequest
- ciSkip
- setBuildDescription
- addNoteOnMergeRequest
- addCiMessage
- addVoteOnMergeRequest
- acceptMergeRequestOnSuccess
- cancelPendingBuildsOnUpdate
- gitlabBuilds
- acceptGitLabMR
- addGitLabMRComment
