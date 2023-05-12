# Tag

## Bamboo input

```yaml
triggers:
  - tag:
      filter: v\d+\.\d+\.\d+
      description: semver
      conditions:
        - green-plan:
            - PAN-NOD
  - tag:
      filter: foo
      tagInBranch: false
      description: Foo trigger
```

## Transformed Github Action

```yaml
on:
  # condition on 'tag' trigger was not transformed because there is no suitable equivalent in GitHub Actions: [{"green-plan"=>["PAN-NOD"]}]
  push:
    tags:
      - v\d+\.\d+\.\d+
      - foo
```

## Unsupported Options

- `conditions`
- `tagInBranch`
