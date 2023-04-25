# ContinuousIntegration Task

## Azure DevOps Input

```yaml
trigger:
  batch: true # batch changes if true; start a new build for every push if false (default)
  branches:
    include: [main, dev]
    exclude: [user/*]
  tags:
    include: [release-v*]
    exclude: [whoopsie]
  paths:
    include: [app/**/*.cs, test/**/*.cs]
    exclude: ["**/*.csproj"]
```

### Transformed Github Action

```yaml
on:
  push:
    branches:
      - main
      - dev
      - "!user/*"
    tags:
      - release-v*
      - "!whoopsie"
    paths:
      - app/**/*.cs
      - test/**/*.cs
      - "!**/*.csproj"
```

### Unsupported Inputs

- batch
