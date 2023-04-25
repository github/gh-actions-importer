# Release

## GitLab Input

```yaml
release_job:
  ...
  release:
    name: 'Release $CI_COMMIT_TAG'
    description: 'Created using the release-cli $EXTRA_DESCRIPTION'
    tag_name: '$CI_COMMIT_TAG'
    ref: '$CI_COMMIT_TAG'
    milestones:
      - 'm1'
      - 'm2'
      - 'm3'
    released_at: '2020-07-15T08:00:00Z'
    assets:
      links:
        - name: 'asset1'
          url: 'https://example.com/assets/1'
        - name: 'asset2'
          url: 'https://example.com/assets/2'
          filepath: '/pretty/url/1'
          link_type: 'other'
```

### Transformed Github Action

```yaml
- uses: softprops/action-gh-release@v0.1.15
  with:
    tag_name: "${{ github.ref }}"
    body: Created using the release-cli $EXTRA_DESCRIPTION
    name: Release ${{ github.ref }}
    target_commitish: "${{ github.ref }}"
```

### Unsupported Options

- `milestones`
- `released_at`
- `assets`
