# Slack

## Travis Input

```yaml
notifications:
  slack:
    rooms:
      - "my-slack-room"
      template: "hello world"
```

## Transformed Github Action

```yaml
- uses: rtCamp/action-slack-notify@v2.2.0
  env:
    SLACK_WEBHOOK: "chat.freenode.net#my-channel"
    SLACK_CHANNEL: my-slack-room
    SLACK_MESSAGE: "hello world"
```
