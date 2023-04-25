# Docker

## Designer Pipeline

```xml
<com.nirima.jenkins.plugins.docker.DockerJobTemplateProperty plugin="docker-plugin@1.2.1">
  <cloudname/>
  <template>
    <configVersion>2</configVersion>
    <connector class="io.jenkins.docker.connector.DockerComputerAttachConnector"/>
    <instanceCap>2147483647</instanceCap>
    <mode>NORMAL</mode>
    <retentionStrategy class="com.nirima.jenkins.plugins.docker.strategy.DockerOnceRetentionStrategy">
    <idleMinutes>10</idleMinutes>
    </retentionStrategy>
    <dockerTemplateBase>
      <image>my-image</image>
      <bindAllPorts>false</bindAllPorts>
      <cpuPeriod>0</cpuPeriod>
      <cpuQuota>0</cpuQuota>
      <privileged>false</privileged>
      <tty>false</tty>
    </dockerTemplateBase>
    <removeVolumes>false</removeVolumes>
    <stopTimeout>10</stopTimeout>
    <pullStrategy>PULL_ALWAYS</pullStrategy>
    <pullTimeout>300</pullTimeout>
    <disabled>
      <disabledByChoice>true</disabledByChoice>
    </disabled>
  </template>
</com.nirima.jenkins.plugins.docker.DockerJobTemplateProperty>
```

### Transformed Github Action

```yaml
container:
  image: my-image
```

### Unsupported Options

- Docker cloud
- Labels
- Name
- Registry authentication
- Container settings
- Instance capacity
- Remote file system root
- Usage
- Idle timeout
- Connect method
- Stop timeout
- Remove volumes
- Pull strategy
- Pull timeout
- Node properties

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
agent {
    docker {
        image 'maven:3-alpine'
        label 'my-defined-label'
        args  '-v /tmp:/tmp'
        registryUrl 'https://myregistry.com/'
        registryCredentialsId 'myPredefinedCredentialsInJenkins'
    }
}
```

### Transformed Github Action

```yaml
job:
  container:
    image: maven:3-alpine
```

### Unsupported Options

- label
- args
- registryUrl
- registryCredentialsId
