# Scripts

The scripts transformer includes the following jobs:

- Script
- Before Install
- After Install
- Before Script
- After Script
- Before Cache
- Before Deploy
- After Deploy

## Travis input

```yaml
script: npm run lint
```

### Transformed Github Action

```yaml
- run: npm run lint
```
