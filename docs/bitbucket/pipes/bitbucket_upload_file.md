# Bitbucket Upload File

[BitBucket Bitbucket Upload File Documentation](https://bitbucket.org/atlassian/bitbucket-upload-file)

## Bitbucket Input

```yaml
pipe: atlassian/bitbucket-upload-file:0.5.0
variables:
  BITBUCKET_USERNAME: $BITBUCKET_USERNAME
  BITBUCKET_APP_PASSWORD: $BITBUCKET_APP_PASSWORD
  FILENAME: '*.html'
```

## Transformed GitHub Action
```yaml
uses: actions/upload-artifact@v3.1.1
with:
  path: "*.html"
```

## Unsupported Options
- DEBUG
- ACCOUNT
- REPOSITORY
- BITBUCKET_USERNAME
- BITBUCKET_APP_PASSWORD
- BITBUCKET_ACCESS_TOKEN