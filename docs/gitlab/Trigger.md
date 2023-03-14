# Trigger

## Parent child pipelines

```yaml
trigger:
  include:
  - local: path/to/child-pipeline.yml
```

### Transformed GitHub action

The job graph of the included files is merged with the job graph of the calling workflow.

## Multi-project pipelines

```yaml
trigger:
  "org/repo"
```

### Transformed GitHub action

```yml
- name: Run downstream workflow
  run: gh workflow run $WORKFLOW_FILE --repo org/repo
  env:
    WORKFLOW_FILE: UPDATE_ME
```

### Unsupported options

None
