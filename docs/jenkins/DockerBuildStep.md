# Docker Build Step

## "Create/build image" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
    <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.CreateImageCommand">
        <dockerFolder>$WORKSPACE/docker</dockerFolder>
        <imageTag>project:$BUILD_NUMBER</imageTag>
        <dockerFile>Dockerfile</dockerFile>
        <pull>true</pull>
        <noCache>true</noCache>
        <rm>true</rm>
        <buildArgs>http_proxy=http://1.2.3.4:4321;foo=bar</buildArgs>
    </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker build/create image
  shell: bash
  run: docker build --build-arg http_proxy=http://1.2.3.4:4321 --build-arg foo=bar --pull --no-cache --rm -t project:$BUILD_NUMBER -f $WORKSPACE/docker/Dockerfile
```

### Unsupported Options

None

## "Pull image" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
    <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.PullImageCommand">
        <dockerRegistryEndpoint plugin="docker-commons@1.17"/>
        <fromImage>redis</fromImage>
        <tag>6</tag>
        <registry>registry.hub.docker.com</registry>
    </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- uses: docker/login-action@v2.1.0
  with:
    registry: registry.hub.docker.com
    username: "${{ secrets.DOCKER_USERNAME }}"
    password: "${{ secrets.DOCKER_PASSWORD }}"
- name: Run docker pull image
  run: docker pull registry.hub.docker.com/redis:6
```

### Unsupported Options

None

## "Push image" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
    <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.PushImageCommand">
        <dockerRegistryEndpoint plugin="docker-commons@1.17"/>
        <image>my/redis</image>
        <tag>latest</tag>
        <registry>registry.hub.docker.com</registry>
    </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- uses: docker/login-action@v2.1.0
  with:
    registry: registry.hub.docker.com
    username: "${{ secrets.DOCKER_USERNAME }}"
    password: "${{ secrets.DOCKER_PASSWORD }}"
- name: Run docker push image
  run: docker push registry.hub.docker.com/my/redis:latest
```

### Unsupported Options

None

## "Tag image" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
    <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.TagImageCommand">
        <image>redis:6</image>
        <repository>my/redis</repository>
        <tag>latest</tag>
        <ignoreIfNotFound>true</ignoreIfNotFound>
        <withForce>false</withForce>
    </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker tag image
  run: docker tag redis:6 my/redis:latest
```

### Unsupported Options

- withForce

## "Create container" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.CreateContainerCommand">
    <image>ubuntu:latest</image>
    <command>/bin/bash</command>
    <hostName>my-host</hostName>
    <containerName>sleepy-jenkins</containerName>
    <envVars>foo=bar bar=baz</envVars>
    <links>container1:anothercontainer, container2:some-db</links>
    <exposedPorts>9000/tcp</exposedPorts>
    <cpuShares>50</cpuShares>
    <memoryLimit>1024m</memoryLimit>
    <dns>8.8.8.8,1.1.1.1</dns>
    <extraHosts>phonehome.example.com:127.0.0.1,myhost:127.0.01</extraHosts>
    <networkMode>host</networkMode>
    <publishAllPorts>false</publishAllPorts>
    <portBindings>80 8080 80:8080 10.0.47.11:80 8080/tcp 10.0.47.11:80:8080/udp </portBindings>
    <bindMounts>data /data rw /hostpath /containerpath ro</bindMounts>
    <privileged>true</privileged>
    <alwaysRestart>true</alwaysRestart>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
    - name: Run docker create container
      run: |-
        docker create \
        --hostname my-host \
        --name sleepy-jenkins \
        --cpu-shares 50 \
        --memory-limit 1024m \
        --network host \
        --privileged \
        --always-restart \
        -e foo=bar \
        -e bar=baz \
        --link container1:anothercontainer \
        --link  container2:some-db \
        --expose 9000/tcp \
        --dns 8.8.8.8 \
        --dns 1.1.1.1 \
        --add-host phonehome.example.com:127.0.0.1 \
        --add-host myhost:127.0.01 \
        -p 80:8080 \
        -p 80:8080 \
        -p 10.0.47.11:80:8080/tcp \
        -p 10.0.47.11:80:8080/udp \
        -v data:/data:rw \
        -v /hostpath:/containerpath:ro \
        ubuntu:latest \
        "/bin/bash"
```

### Unsupported Options

None

## "Save image" Designer Pipeline

### Jenkins Input

```xml
<dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.SaveImageCommand">
  <imageName>busybox</imageName>
    <imageTag>latest</imageTag>
    <destination>/tmp</destination>
    <filename>busybox.tar</filename>
    <ignoreIfNotFound>false</ignoreIfNotFound>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker save image
  run: docker save -o /tmp/busybox.tar busybox:latest
- uses: actions/upload-artifact@v2
  with:
    path: "/tmp/busybox.tar"
```

### Unsupported Options

None

## "Commit container" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.CommitCommand">
    <containerId>a6757a514067</containerId>
    <repo>registry:5000/repo</repo>
    <tag>ubuntu:latest</tag>
    <runCmd>ping -c 4 8.8.8.8</runCmd>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker commit
  run: docker commit --change='CMD ping -c 4 8.8.8.8' a6757a514067 registry:5000/repo/ubuntu:latest
```

### Unsupported Options

None

## "Stop all containers" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.StopAllCommand"/>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker stop all containers
  shell: bash
  run: docker stop $(docker ps -q)
```

### Unsupported Options

None

## "Stop container(s)" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.StopCommand">
    <containerIds>6d4a94ec49b0</containerIds>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker stop containers by id
  run: docker stop 6d4a94ec49b0
```

### Unsupported Options

None

## "Stop container(s) by image ID" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.StopByImageIdCommand">
   <imageId>2cfd4a4587bc</imageId>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker stop containers by image id
  shell: bash
  run: docker stop $(docker ps -q -f ancestor=2cfd4a4587bc)
```

### Unsupported Options

None

## "Kill container(s)" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.KillCommand">
    <containerIds>6d4a94ec49b0</containerIds>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker kill containers by id
  run: docker kill 6d4a94ec49b0
```

### Unsupported Options

None

## "Restart container(s)" Designer Pipeline

### Jenkins Input

```xml
<dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.RestartCommand">
    <containerIds>6d4a94ec49b0</containerIds>
    <timeout>0</timeout>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker restart containers by id
  run: docker restart 6d4a94ec49b0
```

### Unsupported Options

None

## "Start container(s)" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.StartCommand">
    <containerIds>6d4a94ec49b0</containerIds>
    <waitPorts/>
    <containerIdsLogging/>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker start containers by id
  run: docker start 6d4a94ec49b0
```

### Unsupported Options

- containerIdsLogging
- waitPorts

## "Start container(s) by image ID" Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.dockerbuildstep.DockerBuilder plugin="docker-build-step@2.5">
  <dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.StartByImageIdCommand">
    <imageId>5992a3bf97a3</imageId>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker start containers by image id
  shell: bash
  run: docker start $(docker ps -q -f ancestor=5992a3bf97a3)
```

### Unsupported Options

None

## "Create and start exec instance in container(s)" Designer Pipeline

### Jenkins Input

```xml
<dockerCmd class="org.jenkinsci.plugins.dockerbuildstep.cmd.ExecCreateAndStartCommand">
    <containerIds>6d4a94ec49b0,a6757a514067</containerIds>
    <command>ping -c 4 8.8.8.8</command>
  </dockerCmd>
</org.jenkinsci.plugins.dockerbuildstep.DockerBuilder>
```

### Transformed Github Action

```yaml
- name: Run docker exec create and start
  run: |-
    docker exec 6d4a94ec49b0 ping -c 4 8.8.8.8
    docker exec a6757a514067 ping -c 4 8.8.8.8
```

### Unsupported Options

None

## Jenkinsfile Pipeline

This plugin is not mapped to a GitHub Actions equivalent for a Jenkinsfile Pipeline.
