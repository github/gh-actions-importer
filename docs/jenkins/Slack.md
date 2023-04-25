# Slack Notification

## Designer Pipeline

```xml
<jenkins.plugins.slack.SlackNotifier plugin="slack@631.v40deea_40323b">
  <baseUrl>https://myinstance.com</baseUrl>
  <teamDomain/>
  <authToken/>
  <tokenCredentialId/>
  <botUser>false</botUser>
  <room>#channel</room>
  <sendAsText/>
  <iconEmoji>:shipit:</iconEmoji>
  <username>jenkins-integration</username>
  <startNotification>true</startNotification>
  <notifySuccess>true</notifySuccess>
  <notifyAborted>true</notifyAborted>
  <notifyNotBuilt/>
  <notifyUnstable/>
  <notifyRegression/>
  <notifyFailure>true</notifyFailure>
  <notifyEveryFailure>false</notifyEveryFailure>
  <notifyBackToNormal>false</notifyBackToNormal>
  <notifyRepeatedFailure>false</notifyRepeatedFailure>
  <includeTestSummary/>
  <includeFailedTests/>
  <uploadFiles>true</uploadFiles>
  <artifactIncludes>artifacts/</artifactIncludes>
  <commitInfoChoice/>
  <includeCustomMessage>true</includeCustomMessage>
  <customMessage>Default Custom Message</customMessage>
  <customMessageSuccess>Custom Success Message</customMessageSuccess>
  <customMessageAborted>Custom Cancelled Message</customMessageAborted>
  <customMessageNotBuilt/>
  <customMessageUnstable/>
  <customMessageFailure>Custom Failure Message</customMessageFailure>
</jenkins.plugins.slack.SlackNotifier>
```

### Partially Supported Options
The following options are only supported if you use the deprecated [Incoming Webhooks](https://slack.com/apps/A0F7XDUAZ-incoming-webhooks) Slack App instead of the recommended approach of enabling webhooks in a custom Slack App.

- room
- iconEmoji
- username

### Unsupported Options

- teamDomain
- authToken
- tokenCredentialId
- notifyNotBuilt
- notifyUnstable
- notifyRegression
- includeTestSummary
- includeFailedTests
- commitInfoChoice
- customMessageNotBuilt
- customMessageUnstable

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
    slackSend channel: '#general',
        color: COLOR_MAP[currentBuild.currentResult],
        message: "Hello World!!"
```

### Transformed Github Action

```yaml
- name: Slack Notification
  uses: rtCamp/action-slack-notify@v2.2.0
  env:
     SLACK_WEBHOOK: "${{ secrets.SLACK_WEBHOOK }}"
     SLACK_CHANNEL: "#general"
     SLACK_MESSAGE: Hello World!!

### Unsupported Options

- SLACK_MSG_AUTHOR
- SLACK_ICON
- SLACK_ICON_EMOJI
- SLACK_COLOR
- SLACK_TITLE
- SLACK_FOOTER
- MSG_MINIMAL
