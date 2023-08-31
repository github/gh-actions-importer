# Kubernetes

## Designer Pipeline

This plugin is not available on Designer Pipelines.

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
agent {
  kubernetes {
    defaultContainer 'maven'
    customWorkspace 'some/other/path'
    yaml '''
      apiVersion: v1
      kind: Pod
      spec:
        containers:
        - name: maven
          image: maven:alpine
          command:
          - sleep
          tty: true
        - name: busybox
          image: busybox
          command:
          - sleep
      '''
  }
}

steps {
  sh 'mvn -version'

  container('busybox') {
    sh '/bin/busybox'
  }
}
```

### Transformed Github Workflow

```yaml
run:
  runs-on: ubuntu-latest
  container:
    image: maven:alpine
    options:
      tty: true
      workdir: some/other/path
  steps:
  - name: checkout
    uses: actions/checkout@v3.5.0
  - name: sh
    shell: bash
    run: mvn -version
  # - The `container` step was transformed into the `busybox_1b1dce03-557d-4c79-83dc-2b31e15e04ae` job
  #   which runs child steps in the corresponding container. Ensure that all steps that depend on this job,
  #   such as accessing build artifacts, also run within the `busybox_1b1dce03-557d-4c79-83dc-2b31e15e04ae` job.
busybox_1b1dce03-557d-4c79-83dc-2b31e15e04ae:
  container:
    image: busybox
  steps:
  - name: sh
    shell: bash
    run: "/bin/busybox"
```

### Unsupported Options

- None
