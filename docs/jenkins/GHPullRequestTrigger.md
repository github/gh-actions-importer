# GitHub Pull Request Trigger

## Designer Pipeline

### Jenkins Input

```xml
  <org.jenkinsci.plugins.ghprb.GhprbTrigger>
    <spec>H/5 * * * *</spec>
    <configVersion>3</configVersion>
    <adminlist />
    <allowMembersOfWhitelistedOrgsAsAdmin>false</allowMembersOfWhitelistedOrgsAsAdmin>
    <orgslist />
    <cron>H/5 * * * *</cron>
    <buildDescTemplate />
    <onlyTriggerPhrase>false</onlyTriggerPhrase>
    <useGitHubHooks>false</useGitHubHooks>
    <permitAll>true</permitAll>
    <whitelist />
    <autoCloseFailedPullRequests>false</autoCloseFailedPullRequests>
    <displayBuildErrorsOnDownstreamBuilds>false</displayBuildErrorsOnDownstreamBuilds>
    <whiteListTargetBranches>
        <org.jenkinsci.plugins.ghprb.GhprbBranch>
          <branch>/main</branch>
        </org.jenkinsci.plugins.ghprb.GhprbBranch>
    </whiteListTargetBranches>
    <blackListTargetBranches>
        <org.jenkinsci.plugins.ghprb.GhprbBranch>
          <branch>/bad</branch>
        </org.jenkinsci.plugins.ghprb.GhprbBranch>
    </blackListTargetBranches>
    <gitHubAuthId>0d0af9bb-ddec-4f5f-8428-1f02c4b119e7</gitHubAuthId>
    <triggerPhrase />
    <skipBuildPhrase>.*\\[skip\\W+ci\\].*</skipBuildPhrase>
    <blackListCommitAuthor />
    <blackListLabels>dont-trigger-this</blackListLabels>
    <whiteListLabels />
    <includedRegions />
    <excludedRegions />
    <extensions>
        <org.jenkinsci.plugins.ghprb.extensions.status.GhprbSimpleStatus>
          <commitStatusContext />
          <triggeredStatus /> 
          <startedStatus />   
          <statusUrl />
          <addTestResults>false</addTestResults>
        </org.jenkinsci.plugins.ghprb.extensions.status.GhprbSimpleStatus>
    </extensions>
  </org.jenkinsci.plugins.ghprb.GhprbTrigger>
```

### Transformed Github Action

```yaml
   pull_request:
     branches:
     - "/main"
     - "!/bad"
```

### Unsupported Options
- Admin list
- Use github hooks for build triggering
- Allow members of whitelisted organizations as admins
- List of organizations. Their members will be whitelisted
- Crontab line
- Build description template
- Only use trigger phrase for build triggering
- Build every pull request automatically without asking (Dangerous!).
- White list
- Close failed pull request automatically?
- Display build errors on downstream builds?
- Trigger phrase
- Skip build phrase
- Blacklist commit authors
- List of GitHub labels for which the build should not be triggered.
- List of GitHub labels for which the build should only be triggered. (Leave blank for 'any')
- Cancel build on update
- Build status messages
- Comment file
- Update commit status during build

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
