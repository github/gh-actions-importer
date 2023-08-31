# GitHub Pages

## Travis Input

```yaml
deploy: 
  provider: pages:git
  token: "<fake_token>"
  repo: "Valet"
  target_branch: "main"
  keep_history: true
  commit_message: "Deploying a GitHub page"
  allow_empty_commit: true
  verbose : true
  local_dir: "./path"
  fqdn: ".com"
  name: "savy_dev"
  email: "savy_dev@github.com"
  committer_from_gh: true
  deployment_file: true
  url: "website.com"
  strategy: git
  cleanup: true
  run: "echo 'deploying the second version'"
```

## Transformed Github Action

```yaml
  - uses: JamesIves/github-pages-deploy-action@v4.4.1
    with:
      branch: main
      folder: "./path"
      token: "${{ secrets.GITHUB_TOKEN }}"
      commit-message: Deploying a GitHub page
      git-config-name: savy_dev
      git-config-email: savy_dev@github.com
    if: "${{ github.event_name == 'push' && github.ref == 'refs/heads/main' }}"
  - run: echo 'deploying the second version'
    if: "${{ github.event_name == 'push' && success() && github.ref == 'refs/heads/main' }}"

```

### Unsupported Options

- allow_empty_commit
- verbose
- fqdn
- name
- committer_from_gh
- deployment_file
- strategy
- cleanup
- edge
- skip cleanup (deprecated in Travis)
