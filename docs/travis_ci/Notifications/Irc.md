# IRC

## Travis Input

```yaml
notifications:
  irc:
    channels:
      - "chat.freenode.net#my-channel"
      - "chat.freenode.net#some-other-channel"
```

## Transformed Github Action

```yaml
- uses: rectalogic/notify-irc@v1
  with:
    channel: "chat.freenode.net#my-channel"
    nickname: Default nickname
    message: Default message
- uses: rectalogic/notify-irc@v1
  with:
    channel: "chat.freenode.net#some-other-channel"
    nickname: Default nickname
    message: Default message
```

### Unsupported Options

- nickserv_password
- use_notice
- skip_join
