# Repository Commit

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.vcs:task.vcs.commit
    configuration:
      commitMessage: 'Bamboo: ${bamboo.buildResultKey}'
      selectedRepository: defaultRepository
    description: Commit the changes
```

## Transformed Github Action

```yaml
- name: Commit the changes
  run: |-
    git add .
    git commit -m "Bamboo: ${{ github.workflow }}-${{ github.job }}-${{ github.run_id }}"
    git push origin HEAD
```

## Unsupported Options

* selectedRepository
