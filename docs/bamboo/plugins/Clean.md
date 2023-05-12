# Clean

## Bamboo input

```yaml
- clean
```

## Transformed Github Action

```yaml
- name: Clean working directory
  run: rm -rf ${{ github.workspace }}/*
  shell: bash
```

## Unsupported Options
none
