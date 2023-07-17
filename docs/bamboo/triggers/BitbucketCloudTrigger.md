# Bitbucket Cloud Trigger

## Bamboo input

```yaml
triggers:
- bitbucket-cloud-trigger
```

## Transformed Github Action

**Note:** Follow the instructions described in the [repository_dispatch](https://gh.io/actions-repository-dispatch) documentation to setup the webhook event.

```yaml
on:
 repository_dispatch:
```

## Unsupported Options

- None
