# Docker

## CircleCI Input

```yaml
docker:
  - image: "circleci/node:9.6.1"
    auth:
      username: mydockerhub-user
      password: $DOCKERHUB_PASSWORD
```

### Transformed Github Action

```yaml
container:
  image: "circleci/node:9.6.1"
  credentials:
    username: mydockerhub-user
    password: $DOCKERHUB_PASSWORD
```

### Unsupported Options

- aws_auth
- multiple docker images
