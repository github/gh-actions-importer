# Docker Build and Publish

## Designer Pipeline

### Jenkins Input

```xml
<com.cloudbees.dockerpublish.DockerBuilder plugin="docker-build-publish@1.3.2">
  <server plugin="docker-commons@1.17"/>
  <registry plugin="docker-commons@1.17">
    <url>https://index.docker.io/v1/</url>
  </registry>
  <repoName>example/hello-world</repoName>
  <noCache>false</noCache>
  <forcePull>false</forcePull>
  <buildContext>.</buildContext>
  <skipBuild>false</skipBuild>
  <skipDecorate>true</skipDecorate>
  <repoTag>v1.1</repoTag>
  <skipPush>false</skipPush>
  <createFingerprint>true</createFingerprint>
  <skipTagLatest>false</skipTagLatest>
  <buildAdditionalArgs>https_proxy="http://some.proxy:port"</ buildAdditionalArgs>
  <forceTag>false</forceTag>
</com.cloudbees.dockerpublish.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: checkout
  uses: actions/checkout@v2
- name: Login to Docker Hub
  uses: docker/login-action@v2.1.0
  with:
    username: "${{ secrets.DOCKER_USERNAME }}"
    password: "${{ secrets.DOCKER_PASSWORD }}"
    logout: true
    registry: index.docker.io
- name: Build Docker Image
  run: docker build '.' https_proxy="http://some.proxy:port" -t index.docker.io/example/hello-world:v1.1 -t index.docker.io/example/hello-world:latest
- name: Push Docker Image
  run: docker push index.docker.io/example/hello-world:v1.1
- name: Push Docker Image
  run: docker push index.docker.io/example/hello-world:latest
```
### Unsupported Options

- createFingerprint
- forceTag
- skipDecorate
