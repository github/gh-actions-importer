# Npm

## Travis Input

```yaml
deploy:
  provider: npm
  api_token: <encrypted api_token>
  edge: true
```

## Transformed Github Action

```yaml
    - uses: actions/setup-node@v2
      with:
        node-version: 12.x
        registry-url: https://this_place.com
      if: "${{ github.event_name == 'push' && github.ref == 'refs/heads/main' }"
    - run: npm install
      if: "${{ github.event_name == 'push' && github.ref == 'refs/heads/main' }"
    - run: npm publish ./this/file.rb --access restricted --tag v3 --dry-run
      env:
        NODE_AUTH_TOKEN: "${{ secrets.NPM_TOKEN }}"
      if: "${{ github.event_name == 'push' && github.ref == 'refs/heads/main'"
```

### Unsupported Options

- email
- run_script
- auth_method
- edge
- skip cleanup (deprecated in Travis)
