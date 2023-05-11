# Command

## Bamboo input

```yaml
- command:
    executable: foo
    argument: --help
    environment: BAR=bar BAZ=baz
    working-dir: tmp
    conditions:
      - variable:
          exists: ABC
    description: Sample command task
```

## Transformed Github Action

```yaml
- name: Sample command task
  run: foo --help
  working-directory: tmp
  if: env.ABC != ''
```

## Unsupported Options

none
