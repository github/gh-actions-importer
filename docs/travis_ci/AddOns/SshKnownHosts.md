# SSH Known Hosts

## Travis Input

```yaml
addons:
  ssh_known_hosts:
  - git.example.com
  - 111.22.33.44
```

### Transformed Github Action

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - run: ssh-keyscan -H git.example.com >> ~/.ssh/known_hosts
    - run: ssh-keyscan -H 111.22.33.44 >> ~/.ssh/known_hosts
    - uses: actions/checkout@v1
```

### Unsupported Options

- None
