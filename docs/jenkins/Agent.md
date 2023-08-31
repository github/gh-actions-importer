# Agent

## Designer Pipeline

### Jenkins Input

Label:
```xml
<assignedNode>
  node-label
</assignedNode>
```

### Transformed Github Action

```yaml
jobs:
  build:
    runs-on: node-label
```

### Supported Agent types
- label agent

### Unsupported Agent types

- any agent
- none agent
- [docker](Docker.md) agent
- [node](Node.md) agent
- dockerfile agent
- kubernetes agent

## Jenkinsfile Pipeline

### Jenkins Input

Top level agent:
```groovy
agent {
  label 'my-defined-label'
}

stages {
  stage('Example Build') {
    ....
  }
}
```

or Stage level agent:
```groovy
stages {
  stage('Example Build') {
    agent {
        label 'my-defined-label'
      }
    ....
  }
}
```

### Transformed Github Action

```yaml
jobs:
  Example-Build:
    name: Example Build
    runs-on: [self-hosted, my-defined-label]
```

### Supported Agent types
- any agent
- none agent
- label agent
  - And conditionals
- [docker](Docker.md) agent
- [node](Node.md) agent

### Unsupported Agent types

- dockerfile agent
- kubernetes agent
