# Bundler

## Travis Input

```yaml
cache:
  bundler: true
```

## Transformed Github Action

The bundler cache is transformed by updating any existing setup-ruby steps with `bundler_cache: true`, or by adding a new setup-ruby step to the workflow.

```yaml
- uses: ruby/setup-ruby@v1.138.0
  with:
    ruby-version: head
    bundler-cache: true
```
