# Parameters

## CircleCI Input

```yaml
parameters:
  example-parameter:
    type: string
    default: "latest"
  example-parameter-two:
    type: string
    description: "my example parameter"
```

### Transformed Github Action

```yaml
workflow_dispatch:
  inputs:
    example-parameter:
      required: false
      default: latest
    example-parameter-two:
      required: true
      description: "my example parameter"
```

### Unsupported Options

- Type
