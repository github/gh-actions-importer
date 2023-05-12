# Stop Job

## Stopping with a success state

### Bamboo input

```yaml
- stop-job:
    success: 'true'
    conditions:
    - variable:
        exists: should_exist
    description: Woah slow down!
```

### Transformed Github Action

```yaml
  # stop-job with success isn't natively supported in GitHub Actions.
  # To support this behavior, the steps after this task will use
  # this task's condition(s) to conditionally skip them.
  # See https://github.com/actions/runner/issues/662
  - id: stop-job-1849
    name: Woah slow down!
    run: stop=${{ env.should_exist != '' }} >> $GITHUB_OUTPUT && exit 0
    if: env.should_exist != ''
  - name: Another step
    run: my-script
    # The stop-job output is used to conditionally skip this step, in addition to its own condition
    if: ${{ steps.stop-job-1849.outputs.stop == 'false' && other.condition == 'foo' }}
```

## Stopping with a failure state

### Bamboo input

```yaml
- stop-job:
  conditions:
  - variable:
      exists: should_exist
  description: Woah slow down!
```

### Transformed Github Action

```yaml
- name: Woah slow down!
  run: exit 1
  if: env.should_exist != ''
```

## Unsupported Options
