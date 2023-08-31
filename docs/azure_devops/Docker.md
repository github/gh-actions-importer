# Docker Task

## Azure DevOps Input

### Login

V1

```yaml
- task: Docker@1
  inputs:
    containerRegistryType: 'Container Registry'
    dockerRegistryEndpoint: 'GitHubContainerRegistryConnection'
    command: 'login'
```

V2

```yaml
- task: Docker@2
  displayName: Login to ACR
  inputs:
    command: login
    containerRegistry: GitHubContainerRegistryConnection
```

#### Transformed Github Action for login

```yaml
env:
  GITHUBCONTAINERREGISTRY_DOCKER_REGISTRY: ghcr.io

jobs:
  __default-Job:
  - uses: docker/login-action@v2.1.0
    with:
      registry: "${{ env.GITHUBCONTAINERREGISTRY_DOCKER_REGISTRY }}"
      username: "${{ env.GITHUBCONTAINERREGISTRY_DOCKER_USERNAME }}"
      password: "${{ secrets.GITHUBCONTAINERREGISTRY_DOCKER_PASSWORD }}"
```

### Logout

V1

```yaml
- task: Docker@1
  inputs:
    command: 'logout'
```

V2

```yaml
- task: Docker@2
  inputs:
    containerRegistry: dockerRegistryServiceConnection
    command: 'logout'
```

#### Transformed Github Action for logout

```yaml
-run: docker logout '${{ env.GITHUBCONTAINERREGISTRY_DOCKER_REGISTRY }}'
```

### Build

If no `dockerfile` parameter is passed (defualt value: `**/Dockerfile`) or `**/Dockerfile`) is used we automatically convert it to `Dockerfile`, this will break some
pipelines but we emit a manual task to check if fixing is needed.

V0

```yaml
- task: Docker@0
  inputs:
    command: 'Build an image'
    dockerFile: '**/Dockerfile'
    imageName: '$(Build.Repository.Name):$(Build.BuildId)'
    # containerRegistryType: # 'Azure Container Registry' || 'Container Registry`
    # dockerRegistryEndpoint: # if containerRegistryType == 'Container Registry'
    # azureSubscriptionEndpoint # if containerRegistryType == 'Azure Container Registry'
    # azureContainerRegistry # if containerRegistryType == 'Azure Container Registry'
    # qualifyImageName: true # default true
    # buildArguments:
    # includeSourceTags: false
    # includeLatestTag: false
    # useDefaultContext: true
    # context: 
    # additionalImageTags:
```

V1

```yaml
- task: Docker@1
  inputs:
    command: 'Build'
    dockerFile: '**/Dockerfile'
    imageName: '$(Build.Repository.Name):$(Build.BuildId)'
    # containerRegistryType: # 'Azure Container Registry' || 'Container Registry`
    # dockerRegistryEndpoint: # if containerRegistryType == 'Container Registry'
    # azureSubscriptionEndpoint # if containerRegistryType == 'Azure Container Registry'
    # azureContainerRegistry # if containerRegistryType == 'Azure Container Registry'
    # qualifyImageName
    # arguments:
    # includeSourceTags: false
    # includeLatestTag: false
    # useDefaultContext: true
    # buildContext: 
```

V2

```yaml
- task: Docker@2
  inputs:
    command: 'Build'
    dockerFile: '**/Dockerfile'
    # repository: ''
    # containerRegistry:
    # arguments:
    # includeSourceTags: true
    # buildContext: **
    # tags
```

> The default value for `tags` is automatically mapped from `$(Build.BuildId)` to `${{ github.run_id }}`

#### Transformed Github Action for build

```yaml
-run: docker build . --file "Dockerfile" -t ghcr.io/$(Build.Repository.Name):$(Build.BuildId)
```

### Push

V0

```yaml
- task: Docker@0
  inputs:
    action: 'Push an image'
    imageName: '$(Build.Repository.Name):$(Build.BuildId)'
    # containerRegistryType: # 'Azure Container Registry' || 'Container Registry`
    # dockerRegistryConnection: # if containerRegistryType == 'Container Registry'
    # azureSubscription # if containerRegistryType == 'Azure Container Registry'
    # azureContainerRegistry # if containerRegistryType == 'Azure Container Registry'
    # includeSourceTags: false
```

V1

```yaml
- task: Docker@1
  inputs:
    command: 'push'
    imageName: '$(Build.Repository.Name):$(Build.BuildId)'
    # containerRegistryType: # 'Azure Container Registry' || 'Container Registry`
    # dockerRegistryEndpoint: # if containerRegistryType == 'Container Registry'
    # azureSubscriptionEndpoint # if containerRegistryType == 'Azure Container Registry'
    # azureContainerRegistry # if containerRegistryType == 'Azure Container Registry'
    # qualifyImageName: true # default true
    # arguments: ???
    # includeSourceTags: false
```

V2

```yaml
- task: Docker@2
  inputs:
    command: 'push'
    # repository: ''
    # containerRegistry:
    # arguments:
    # includeSourceTags: true
    # buildContext
    # Dockerfile
    # tags
```

> The default value for `tags` is automatically mapped from `$(Build.BuildId)` to `${{ github.run_id }}`

#### Transformed Github Action for push

```yaml
run: docker push ${{ env.CUSTOM_REGISTRY_DOCKER_REGISTRY }}/hello:latest
```

### Build and Push

V2

```yaml
- task: Docker@2
  inputs:
    command: 'buildAndPush'
    # repository: ''
    # containerRegistry:
    # arguments:
    # includeSourceTags: true
    # Dockerfile: '**/Dockerfile2'
    # addPipelineData: 
    # arguments: 
    # buildContext: ** 
    # tags: $(Build.BuildId)
```

#### Transformed Github Action for build and push

```yaml
- uses: docker/login-action@v2.1.0
  with:
    username: "${{ env.DUMMY_DOCKER_HUB_DOCKER_USERNAME }}"
    password: "${{ secrets.DUMMY_DOCKER_HUB_DOCKER_PASSWORD }}"
- run: docker build . --file "Dockerfile" -t dancingwombat:${{ github.run_id }}
- run: docker push dancingwombat:${{ github.run_id }}
```

### Tag

V0

> `Tag Images` are not supported

V1

```yaml
- task: Docker@1
  inputs:
    command: 'Tag image'
    imageName: '$(Build.Repository.Name):$(Build.BuildId)'
    # arguments: 
    # containerRegistryType: # 'Azure Container Registry' || 'Container Registry`
    # dockerRegistryConnection: # if containerRegistryType == 'Azure Container Registry'
    # azureSubscription # if containerRegistryType == 'Azure Container Registry'
    # azureContainerRegistry # if containerRegistryType == 'Azure Container Registry'
    # qualifyImageName: true
    # qualifySourceImageName: false
    # includeSourceTags: false
```

#### Transformed Github Action for tag

```yaml
- run: docker tag $(Build.Repository.Name):$(Build.BuildId) ghcr.io/$(Build.Repository.Name):$(Build.BuildId)
```

### Docker Run Command

V0

```yaml
- task: Docker@0
  inputs:
    action: 'Run a Docker command'
    customCommand: 'images ls'
```

#### Transformed Github Action for run command

```yaml
run: docker images ls
```

### Docker Run Image

V0

```yaml
- task: Docker@0
  inputs:
    action: 'action = Run an image'
    # containerRegistryType: # 'Azure Container Registry' || 'Container Registry`
    # dockerRegistryEndpoint: # if containerRegistryType == 'Container Registry'
    # azureSubscriptionEndpoint # if containerRegistryType == 'Azure Container Registry'
    # azureContainerRegistry # if containerRegistryType == 'Azure Container Registry'
    # imageName: $(Build.Repository.Name):$(Build.BuildId)
    # qualifyImageName: true
    # containerName: 
    # ports:
    # volumes:
    # envVars:
    # workDir:
    # entrypoint:
    # containerCommand:
    # detached: true
    # restartPolicy: No # no || onFailure || always || unlessStopped
    # restartMaxRetries: # if restartPolicy == onFailure
    # memory:
```

V1

```yaml
- task: Docker@1
  inputs:
    command: 'Run'
    # imageName: $(Build.Repository.Name):$(Build.BuildId)
    # qualifyImageName: true
    # containerName: 
    # ports:
    # volumes:
    # envVars:
    # workingDirectory:
    # entrypointOverride:
    # containerCommand:
    # runInBackground: true
    # restartPolicy: No # no || onFailure || always || unlessStopped
    # maxRestartRetries: 
    # memoryLimit:
```

#### Transformed Github Action for run image

```yaml
run: docker run -d --name mycontainer-p 80:80 --restart no image:295
```


### Start/Stop

Not Supported

### Unsupported Inputs

Docker registries defined with a URL (only hostnames are supported)

- addPipelineData
- memoryLimit
- dockerFile (with minimatch expressions)
- includeSourceTags
- addDefaultLabels
- enforceDockerNamingConvention
- dockerHostEndpoint
- imageDigestFile
- pushMultipleImages
- imageNamesPath
