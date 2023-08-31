# Deploy to container

## Designer Pipeline

### Jenkins Input

```xml
{
    "plugin"    => "deploy@1.16",
    "adapters"  => [
      {
        "hudson.plugins.deploy.tomcat.Tomcat8xAdapter" => {
          "credentialsId" => "*/*.*",
          "url"           => "nil",
          "path"          => "nil"
        }
      },
      {
        "hudson.plugins.deploy.tomcat.Tomcat8xAdapter" => {
          "credentialsId" => "*/*.*",
          "url"           => "nil",
          "path"          => "nil"
        }
      }
    ],
    "war"       => "*/*.war",
    "onFailure" => "false"
  }
```

### Transformed Github Action

```yaml
- name: run command
  shell: bash
  run: |-
    mvn cargo:undeploy
    mvn clean
    mvn install
    mvn cargo:deploy
  if: always()

```

### Unsupported Options

- onFailure
