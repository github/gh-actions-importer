# Npm Publish

[BitBucket Npm Publish Documentation](https://bitbucket.org/atlassian/npm-publish)

## Bitbucket Input

```yaml
- pipe: atlassian/npm-publish:0.3.3
  variables:
    NPM_TOKEN: 'my_token'
    FOLDER: 'my/dist' # Optional.
    EXTRA_ARGS: '--tag 1.2.3 --dry-run' # optional
    NPM_REGISTRY_AUTH_URL: 'https://my-registry.com' # Optional.
    # DEBUG: '<boolean>' # Optional.
```

## Transformed GitHub Action
```yaml
- uses: actions/setup-node@v3.7.0
  with:
    registry-url: foo
- run: npm publish my/dist --tag 1.2.3 --dry-run
  env:
    NODE_AUTH_TOKEN: "${{ secrets.NPM_TOKEN }}"
```

## Unsupported Options
* DEBUG
