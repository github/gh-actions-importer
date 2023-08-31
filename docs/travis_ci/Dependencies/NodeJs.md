# Node Js

## Travis Input

```yaml
node_js: 7
```

## Transformed Github Action

```yaml
- uses: actions/setup-node@v2
  with:
    cache: npm
    node-version: '12'
    
```

## Unsupported Options

- string node_js versions
