# Virtual Env Builder

## Designer pipeline

### Jenkins input

```xml
<jenkins.plugins.shiningpanda.builders.VirtualenvBuilder plugin="shiningpanda@0.24">
   <pythonName>DemoVersion</pythonName>
   <home />
   <clear>false</clear>
   <systemSitePackages>false</systemSitePackages>
   <nature>shell</nature>
   <command>python setup.py install</command>
   <ignoreExitCode>false</ignoreExitCode>
</jenkins.plugins.shiningpanda.builders.VirtualenvBuilder>
```

### Transformed Github Action

```yaml
- name: set up python
  uses: actions/setup-python@v2
  with:
    python-version: DemoVersion
- name: run python script
  shell: bash
  run: python setup.py install
```

### Unsupported Options
- home
- clear
- systemSitePackages

## Jenkinsfile pipeline

### Jenkins input

```groovy
  stage('build') {
      steps {
          virtualenv {
            clear(true)
            command("python setup.py install")
            ignoreExitCode(true)
            name("Demo environment")
            nature("shell")
            pythonName("python3")
            systemSitePackages(true)
          }
      }
  }
```

```groovy
  stage('build') {
      steps {
        withPythonEnv('python3') {
            sh 'python setup.py install'
        }
      }
  }
```

### Transformed Github Action

```yaml
- name: set up python
  uses: actions/setup-python@v2
  with:
    python-version: DemoVersion
- name: run python script
  shell: bash
  run: python setup.py install

```

### Virtualenv Unsupported Options
- systemSitePackages
- clear

### WithPythonEnv Unsupported Options
- None
