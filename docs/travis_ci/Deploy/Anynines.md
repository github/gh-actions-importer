# Anynines

## Travis Input

```yaml
deploy:
  provider: anynines
  username: <username>
  password: <encrypted password>
  organization: <organization>
  space: <space>
  edge: true
```

## Transformed Github Action

```yaml
- run: cf api https://api.de.a9s.eu-u octocat-p super-secret-o github-s and time
  if: "${{ github.event_name != 'pull_request' && github.ref == 'refs/heads/main' }}"
```

### Unsupported Options

- buildpack
- skip cleanup (deprecated in Travis)
