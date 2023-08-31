# Deploy

## CircleCI Input

```yaml
steps:
- deploy:
    command: echo hello world!
```

### Transformed Github Action

```yaml
- run: echo hello world!
```

### Unsupported Options

- The deploy step will behave the same way as the run step in CircleCI. The two exceptions listed in the documentation [here](https://circleci.com/docs/2.0/configuration-reference/?section=reference#deploy-deprecated) are not supported by Valet.
