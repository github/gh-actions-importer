# Pages

## GitLab Input

```yaml
pages:
  stage: deploy
  script:
    - echo 'Nothing to do...'
  artifacts:
    paths:
      - public
    expire_in: 1 day
```

### Transformed Github Action

```yaml
- uses: JamesIves/github-pages-deploy-action@v4.4.1
  with:
    branch: gh-pages
    folder: public
```

### Unsupported Options

None
