# Reverse Build Trigger

## Designer Pipeline

Visible on the UI as `Build after other projects are built`

### Jenkins Input

```xml
    <jenkins.triggers.ReverseBuildTrigger>
      <spec></spec>
      <upstreamProjects>freestyle leopard, freestyle-aardvark</upstreamProjects>
      <threshold>
        <name>SUCCESS</name>
        <ordinal>0</ordinal>
        <color>BLUE</color>
        <completeBuild>true</completeBuild>
      </threshold>
    </jenkins.triggers.ReverseBuildTrigger>
```

### Transformed Github Action

```yaml
on:
  workflow_run:
    workflows:
    - freestyle leopard
    - freestyle-aardvark
    types:
      - completed
```

### Unsupported Options

- threshold (when `Trigger even if the build is unstable`)

## Jenkinsfile Pipeline

This plugin is not supported in Jenkinsfile Pipelines.
