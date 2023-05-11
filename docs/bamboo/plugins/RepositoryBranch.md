# Repository Branch

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.vcs:task.vcs.branching
    configuration:
      selectedRepository: defaultRepository
      branchName: branch_${bamboo.buildResultKey}
    description: Branch it!
```

## Transformed Github Action

```yaml
- name: Branch it!
  run: |
    git checkout -b branch_${{ github.workflow }}-${{ github.job }}-${{ github.run_id }}
    git push --set-upstream origin branch_${{ github.workflow }}-${{ github.job }}-${{ github.run_id }}
```

## Unsupported Options

* selectedRepository
