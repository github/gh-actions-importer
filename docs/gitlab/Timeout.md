# Timeout

## GitLab Input

- Timeout can be configured in either general pipeline settings or in the pipeline yaml.
- Pipeline timeout overrides the general configuration `build_timeout`.


```yaml
timeout: 1 hundred 20 seconds
```

```ruby
# If configuration timeout is set to 6 hours or equivalent no timeout field will be added
{ build_timeout: 21600 }
```

```ruby
# If configuration timeout is set to any other time besides 6 hours or equivalent a timeout field will be added
{ build_timeout: 360 }
```

### Transformed Github Action

```yaml
timeout-minutes: 2
```

```yaml
# None
```

```yaml
timeout-minutes: 6
```

### Unsupported Options

- None
