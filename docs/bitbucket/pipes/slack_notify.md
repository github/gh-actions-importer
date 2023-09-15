# Slack Notify

[BitBucket Slack Notify Documentation](https://bitbucket.org/atlassian/slack-notify)

## Bitbucket Input

```yaml
# with payload file
- pipe: atlassian/slack-notify:2.1.0
  variables:
    WEBHOOK_URL: 'https://hooks.slack.com/services/T00000000/B00000000/XXXXXXXXXXXXXXXXXXXXXXXX'
    PAYLOAD_FILE: 'payload.json'
# With pretext and message
- pipe: atlassian/slack-notify:2.1.0
  variables:
    WEBHOOK_URL: 'https://hooks.slack.com/services/T00000000/B00000000/XXXXXXXXXXXXXXXXXXXXXXXX'
    PRETEXT: 'Pre-hello'
    MESSAGE: 'Hello!'
```

## Transformed GitHub Action
```yaml
- uses: slackapi/slack-github-action@v1.24.0
  env:
    SLACK_WEBHOOK_URL: "${{ secrets.SLACK_WEBHOOK_URL }}"
    SLACK_WEBHOOK_TYPE: INCOMING_WEBHOOK
  with:
    payload-file-path: payload.json
- uses: slackapi/slack-github-action@v1.24.0
  env:
    SLACK_WEBHOOK_URL: "${{ secrets.SLACK_WEBHOOK_URL }}"
    SLACK_WEBHOOK_TYPE: INCOMING_WEBHOOK
  with:
    payload: |
      {
        "blocks": [
          {
            "type": "section",
            "text": {
              "type": "mrkdwn",
              "text": Pre-hello
            }
          },
          {
            "type": "divider"
          },
          {
            "type": "section",
            "text": {
              "type": "mrkdwn",
              "text": Hello!
            }
          }
        ]
      }
```

## Unsupported Options
* DEBUG
