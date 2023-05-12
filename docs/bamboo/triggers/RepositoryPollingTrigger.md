# Repository Polling Trigger

## Bamboo input

```yaml
triggers:
  - polling:
      cron: 0 0 8 ? * *
      description: My scheduled trigger
      repositories:
        - .NET Sample
      conditions:
        - green-plan:
            - PAN-NOD
  - polling:
      period: "5400"
      description: Another trigger
      repositories:
        - yaml-test-repo
```

## Transformed Github Action

```yaml
on:
  # condition on 'polling' trigger was not transformed because there is no suitable equivalent in GitHub Actions: [{"green-plan"=>["PAN-NOD"]}]
  schedule:
    - cron: 0 8 * * *
    # The following schedule was transformed and may behave differently than in Bamboo.
    # In GitHub Actions, this workflow will run on this schedule regardless of any changes whereas in Bamboo a job will only run if there are changes.
    - cron: 30 1 * * *
```

## Unsupported Options

- `conditions`
- `repositories` - Within Bamboo it is possible to conditionally trigger a build when changes are detected against one or more repositories from various providers. Conditions on triggers are not currently supported. The transformed build will always be run on the specified schedule.
