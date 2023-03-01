# script

## GitLab input

```yaml
script:
  - uname -a
  - bundle exec rspec
```

### Transformed Github Action

```yaml
- run: unname -a
- run: bundle exec rspec
```

### Unsupported Options

None
