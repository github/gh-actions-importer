# Release

## Travis Input

```yaml
deploy:
  provider: releases
  user: "GITHUB USERNAME"
  password: "GITHUB PASSWORD"
  file:
    - file-1
    - file-2
  draft: true
  overwrite: true
  tag_name: v2
  skip_cleanup: true
  prerelease: true
  name: Version dos
  body: This is the body
  on:
    tags: true
```

## Transformed Github Action

```yaml
- uses: softprops/action-gh-release@v0.1.15
  env:
    GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
    GITHUB_REPOSITORY: "${{ github.repository }}"
  with:
    files: |-
      file-1
      file-2
    prerelease: true
    body: This is the body
    draft: true
    tag_name: v2
    name: Version dos
  if: "${{ github.event_name != 'pull_request' }}"
```

### Unsupported Options

- skip cleanup (deprecated)
- edge
