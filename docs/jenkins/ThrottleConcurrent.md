# Throttle Concurrent Builds

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.throttleconcurrents.ThrottleJobProperty plugin="throttle-concurrents@2.3">
    <maxConcurrentPerNode>1</maxConcurrentPerNode>
    <maxConcurrentTotal>2</maxConcurrentTotal>
    <categories class="java.util.concurrent.CopyOnWriteArrayList">
        <string>cat1</string>
    </categories>
    <throttleEnabled>true</throttleEnabled>
    <throttleOption>project</throttleOption>
    <limitOneJobWithMatchingParams>false</limitOneJobWithMatchingParams>
    <paramsToUseForLimit/>
</hudson.plugins.throttleconcurrents.ThrottleJobProperty>
```

### Transformed Github Action

```yaml
concurrency:
  # Note: 'concurrency' may not be supported on GitHub Server instances
  group: "${{ github.workflow }}"
```

### Unsupported Options

The following options are not supported:

- Maximum Total Concurrent Builds
- Maximum Concurrent Builds Per Node
- Prevent multiple jobs with identical parameters from running concurrently

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
pipeline {
    agent any

    options {
      throttleJobProperty(
          categories: ['cat1', 'cat2'],
          throttleEnabled: true,
          throttleOption: 'category'
      )
    }

    stages {
        stage('sleep') {
            steps {
                sh "sleep 100"
                echo "Done"
            }
        }
    }
}
```

### Transformed Github Action

```yaml
concurrency:
  # Note: 'concurrency' may not be supported on GitHub Server instances
  group: cat1-cat2
```

### Unsupported Options

The following options are not supported:
- maxConcurrentTotal
- maxConcurrentPerNode

