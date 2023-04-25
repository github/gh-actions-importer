# Checkout Task

## Azure DevOps Input

```yaml
- checkout: self | none | repository name # self represents the repo where the initial Pipelines YAML file was found
  #clean: boolean  # if true, run `execute git clean -ffdx && git reset --hard HEAD` before fetching
  #fetchDepth: number  # the depth of commits to ask Git to fetch; defaults to no limit
  #lfs: boolean  # whether to download Git-LFS files; defaults to false
  #submodules: true | recursive  # set to 'true' for a single level of submodules or 'recursive' to get submodules of submodules; defaults to not checking out submodules
  #path: string  # path to check out source code, relative to the agent's build directory (e.g. \_work\1); defaults to a directory called `s`
  #persistCredentials: boolean  # if 'true', leave the OAuth token in the Git config after the initial fetch; defaults to false
```

### Transformed Github Action

```yaml
- uses: actions/checkout@v2
  with:
    repository: repository name
    token: "${{ secrets.CHECKOUT_TOKEN }}"
```

### Unsupported Inputs

This action only supports checking out GitHub repositories.
