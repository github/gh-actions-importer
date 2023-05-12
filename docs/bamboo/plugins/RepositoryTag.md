# Repository Tag

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.vcs:task.vcs.tagging
    configuration:
      selectedRepository: defaultRepository
      tagName: ${bamboo.buildResultKey}
    description: Tag It!
```

## Transformed Github Action

```yaml
- name: Tag Repository ${{ github.repository }}
  run: |
    git tag ${{ github.workflow }}-${{ github.job }}-${{ github.run_id }}
    git push origin ${{ github.workflow }}-${{ github.job }}-${{ github.run_id }}
```

## Unsupported Options

* selectedRepository
  * Currently checked out repository will be used
