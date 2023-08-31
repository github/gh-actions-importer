# PullRequest trigger

## Azure DevOps Input

```yaml
trigger:
  autoCancel: false # indicates whether additional pushes to a PR should cancel in-progress runs for the same PR. Defaults to true
  branches:
    include: [main, dev]
    exclude: [user/*]
  paths:
    include: [app/**/*.cs, test/**/*.cs]
    exclude: ["**/*.csproj"]
  drafts: false # For GitHub only, whether to build draft PRs, defaults to true
```

### Transformed Github Action

```yaml
on:
  pull_request:
    branches:
      - main
      - dev
      - "!user/*"
    paths:
      - app/**/*.cs
      - test/**/*.cs
      - "!**/*.csproj"
```

### Unsupported Inputs

- autoCancel
- drafts
