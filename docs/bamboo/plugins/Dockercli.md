# Docker CLI

## Bamboo input

```yaml
  - any-task:
      plugin-key: com.atlassian.bamboo.plugins.bamboo-docker-plugin:task.docker.cli
      configuration:
        commandOption: run
        image: ubuntu:latest
        detach: 'false'
        name: nginx
        containerPort_0: '80'
        hostPort_0: '8080'
        serviceWait: 'true'
        serviceUrlPattern: http://localhost:${docker.port}
        serviceTimeout: '120'
        link: 'false'
        envVars: DEBUG=true JAVA_OPTS="-Xmx256m -Xms128m"
        command: echo testing
        workDir: /data
        additionalArgs: --memory="64m"
        hostDirectory_0: tmp/test
        hostDirectory_1: ${bamboo.working.directory}
        containerDataVolume_0: /test_data
        containerDataVolume_1: /data
      description: 'Run: All supported options'
```

## Transformed Github Action

```yaml
- run: docker run --volume tmp/test:/test_data --volume ${{ env.working_directory }}:/data --workdir /data --rm -e DEBUG=true -e JAVA_OPTS="-Xmx256m -Xms128m" --memory="64m" ubuntu:latest echo testing
```

## Unsupported Options

- Detach container: To add similar behavior a [service containers](https://docs.github.com/en/actions/using-containerized-services/about-service-containers) could be added to the job.
- Link to detached containers
