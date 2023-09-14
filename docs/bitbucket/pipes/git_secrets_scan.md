# Git Secrets Scan

[BitBucket Git Secrets Scan Documentation](https://bitbucket.org/atlassian/git-secrets-scan)

While we support a limited transformation of git-secrets-scan, we recommend using [GitHub's built in Secret Scanning](https://docs.github.com/en/code-security/secret-scanning/protecting-pushes-with-secret-scanning) instead.

## Bitbucket Input

```yaml
pipe: atlassian/git-secrets-scan:0.5.0
variables:
  FILES: 'my/glob/**/*.rb'
```

## Transformed GitHub Action
```yaml
uses: actions/checkout@v3.5.0
with:
  path: git-secrets
  repository: awslabs/git-secrets
run: |
  cd git-secrets
  sudo make install
  git secrets --register-aws --global
  cd ..
# This transformed result does custom secret scanning using AWS, however the recommended way
# to do this is to use the GitHub secret scanning feature.
# See https://docs.github.com/en/code-security/secret-scanning/protecting-pushes-with-secret-scanning for more information.
run: git secrets --scan --recursive 'my/glob/**/*.rb'
```

## Unsupported Options
* CUSTOM_PATTERN_ARGS
* FILES_IGNORED
* ANNOTATION_SUMMARY
* ANNOTATION_DESCRIPTION
* DEBUG
