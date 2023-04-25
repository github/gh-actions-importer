# Code Climate

## Travis Input

```yaml
code_climate:
  repo_token:
    secure: encrypted string
```

### Transformed Github Action

```yaml
- run: |-
    curl -L https://github.com/codeclimate/codeclimate/archive/master.tar.gz | tar xvz
    cd codeclimate-* && sudo make install
```

### Unsupported Options

- repo_token
