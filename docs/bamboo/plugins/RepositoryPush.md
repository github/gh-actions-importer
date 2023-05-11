# Repository Push

## Bamboo input

```yaml
plugin-key: com.atlassian.bamboo.plugins.vcs:task.vcs.push
configuration:
  selectedRepository: defaultRepository
description: Push it!
```

## Transformed Github Action

```yaml
- name: Push it!
  run: git push origin HEAD
```

## Unsupported Options

* selectedRepository
