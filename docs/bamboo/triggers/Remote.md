# Remote

## Bamboo input

```yaml
triggers:
  - remote:
      ip: 1.2.3.4
      description: Foo
      conditions:
        - green-plan:
            - PAN-MAV
```

## Transformed Github Action

**Note:** In Bamboo it is possible to configure a remote trigger based on changes to one or more remote repositories. The transformed workflow will only apply to the target repository the Bamboo pipeline is migrated to.

```yaml
on:
  # condition on 'remote' trigger was not transformed because there is no suitable equivalent in GitHub Actions: [{"green-plan"=>["PAN-MAV"]}]
  push:
```

## Unsupported Options

- `conditions`
- `description`
- `ip`
