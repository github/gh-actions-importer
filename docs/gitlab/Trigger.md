# Trigger

## Parent child pipelines

```yaml
trigger:
  include:
  - local: path/to/child-pipeline.yml
```

### Transformed Github Action

The job graph of the included files is merged with the job graph of the calling workflow.

## Multi-project pipelines

```yaml
trigger:
  "org/repo"
```

### Transformed Github Action

```yml
- name: Run downstream workflow
  run: gh workflow run $WORKFLOW_FILE --repo org/repo
  env:
    WORKFLOW_FILE: UPDATE_ME
```

### Unsupported Options

None
