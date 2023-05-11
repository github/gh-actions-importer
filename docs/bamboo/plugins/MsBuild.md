# MsBuild

## Bamboo input

```yaml
- any-task:
     plugin-key: com.atlassian.bamboo.plugin.dotnet:msbuild
     configuration:
       solution: ExampleSolution.sln
       options: /P:UserName=Panda
       label: EchoMSBuild
```

## Transformed Github Action

```yaml
- name: run msbuild
  run: msbuild ExampleSolution.sln /P:UserName=Panda
```

## Unsupported Options
- none