# Cron

## Bamboo input

```json
  {
    "expression": "0 0 1 * * 1-7"
  }
```

## Transformed Github Action

```yaml
on:
 schedule: 
    cron: "0 1 * * 0-6"
```

## Unsupported Options

- Certain syntaxes are unsupported like the following: "0 0 1 ** 1#1"
