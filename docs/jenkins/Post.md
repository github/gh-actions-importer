# Post build actions

## Designer Pipeline

This plugin is implemented as `Post Build Actions`

## Jenkinsfile Pipeline

### Jenkins Input

#### Stages

```groovy
stage ('build') {
      steps {
            ....
      }

      post {
          cleanup {
              echo "cleanup is executed last"
          }
          aborted {
              echo "aborted"
          }
          always {
              echo "always" 
          }
          unstable {
              powershell "write-host aborted"
          }
          failure {
              bat "echo failure"
          }
          unsuccessful {
              sh "echo unsuccessful"
          }
      }
}

#### Pipeline

```groovy
pipeline {
    stages {
        stage ('build') {
          ...
        }
    }
    post { 
        always { 
            echo 'I will always run'
        }
    }
}
```

### Transformed Github Action

#### Stages

If `post` is inside a stage the content inside conditional blocks will be appended to the stage steps with appropriate conditions.

```yaml
jobs:
  build:
    steps:
      ...
      - name: snapshot post build job status
        run: echo "aborted=${{ job.status == 'cancelled' }}" >> $GITHUB_OUTPUT
        id: __post_build
      - name: powershell
        shell: powershell
        run: write-host aborted
        if: steps.__post_build.outputs.aborted == 'true'
        ....
      - name: echo message
        run: echo always
        if: always()
      - name: echo message
        run: echo cleanup is executed last
        if: always()
```

#### Pipelines

If `post` is inside a pipeline then a `Post-Build` job is added (if a pipeline agent is defined it will be respected) and the steps will be executed on that job.

This job will always be executed regardless of the status of the workflow. A failure of a post step may make the worfklow to fail.

```yaml
jobs:
  build:
    steps:
      ...
  Post-Build:
    if: always()
    needs:
      - build
    steps:
      - name: snapshot post build workflow status
        run: |-
          echo "aborted=${{ contains(needs.*.result,'cancelled') }}" >> $GITHUB_OUTPUT
        id: __post_build
      - name: echo message
        run: echo I will run if worfklow was aborted
        if: steps.__post_build.outputs.aborted == 'true'
      - name: echo message
        run: echo I will always run
        if: always()
```

### Unsupported Options

- Conditions:
  - notBuilt
  - regression
  - fixed
  - changed
