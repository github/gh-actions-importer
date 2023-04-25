# Designer Pipeline

## Jenkins Input

```xml
<publishers>
  <hudson.plugins.git.GitPublisher plugin="git@4.4.4">
    <configVersion>2</configVersion>
    <pushMerge>false</pushMerge>
    <pushOnlyIfSuccess>true</pushOnlyIfSuccess>
    <forcePush>false</forcePush>
    <tagsToPush>
      <hudson.plugins.git.GitPublisher_-TagToPush>
        <targetRepoName>origin</targetRepoName>
        <tagName>0.0.1</tagName>
        <tagMessage>This is a tag message. </tagMessage>
        <createTag>true</createTag>
        <updateTag>false</updateTag>
      </hudson.plugins.git.GitPublisher_-TagToPush>
    </tagsToPush>
    <branchesToPush>
      <hudson.plugins.git.GitPublisher_-BranchToPush>
        <targetRepoName>origin</targetRepoName>
        <branchName>main</branchName>
        <rebaseBeforePush>true</rebaseBeforePush>
      </hudson.plugins.git.GitPublisher_-BranchToPush>
    </branchesToPush>
    <notesToPush>
      <hudson.plugins.git.GitPublisher_-NoteToPush>
        <targetRepoName>origin</targetRepoName>
        <noteMsg>I'm a note</noteMsg>
        <noteNamespace>master</noteNamespace>
        <noteReplace>false</noteReplace>
      </hudson.plugins.git.GitPublisher_-NoteToPush>
      <hudson.plugins.git.GitPublisher_-NoteToPush>
        <targetRepoName>origin</targetRepoName>
        <noteMsg>Here's another note.</noteMsg>
        <noteNamespace>master</noteNamespace>
        <noteReplace>true</noteReplace>
      </hudson.plugins.git.GitPublisher_-NoteToPush>
    </notesToPush>
  </hudson.plugins.git.GitPublisher>
</publishers>
```

## Transformed GitHub Action

```yaml
- name: pushing git resources
  shell: bash
  run: |-
    git fetch --tags
    git tag -a -f -m "This is a tag message. " 0.0.1
    git push origin 0.0.1
    git push origin
    git notes --ref=master append -m "I'm a note"
    git push origin refs/notes/*
    git notes --ref=master add -m "Here's another note."
    git push origin refs/notes/*
  if: success()
```

## Unsupported Options

- `pushMerge`
- `branchesToPush`

## Jenkinsfile Pipeline

Jenkinsfile Pipelines do not support the GitPublisher plugin.
