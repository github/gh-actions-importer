# Artifacts

## GitLab Input

```yaml
artifacts:
  expire_in: 1 week
  name: artifact-name
  paths:
    - binaries/
  exclude:
    - binaries/**/*.o
```

### Transformed Github Action

```yaml
- uses: actions/upload-artifact@v2
  if: success()
  with:
    name: "${{ github.job }}"
    retention-days: 7
    path: |-
      binaries/
      !binaries/**/*.o
```

### Unsupported Options

- `expose_as`
- `public`
- `reports`
- `untracked`
