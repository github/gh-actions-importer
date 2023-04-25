# DotNetCoreCLI Task

## Azure DevOps Input

```yaml
# .NET Core
# Build, test, package, or publish a dotnet application, or run a custom dotnet command
- task: DotNetCoreCLI@2
  inputs:
    #command: 'build' # Options: build, push, pack, publish, restore, run, test, custom
    #publishWebProjects: true # Required when command == Publish
    #projects: # Optional
    #custom: # Required when command == Custom
    #arguments: # Optional
    #publishTestResults: true # Optional
    #testRunTitle: # Optional
    #zipAfterPublish: true # Optional
    #modifyOutputPath: true # Optional
    #feedsToUse: 'select' # Options: select, config
    #vstsFeed: # Required when feedsToUse == Select
    #feedRestore: # Required when command == restore. projectName/feedName for project-scoped feed. FeedName only for organization-scoped feed.
    #includeNuGetOrg: true # Required when feedsToUse == Select
    #nugetConfigPath: # Required when feedsToUse == Config
    #externalFeedCredentials: # Optional
    #noCache: false
    restoreDirectory:
    #restoreArguments: # Optional
    #verbosityRestore: 'Detailed' # Options: -, quiet, minimal, normal, detailed, diagnostic
    #packagesToPush: '$(Build.ArtifactStagingDirectory)/*.nupkg' # Required when command == Push
    #nuGetFeedType: 'internal' # Required when command == Push# Options: internal, external
    #publishVstsFeed: # Required when command == Push && NuGetFeedType == Internal
    #publishPackageMetadata: true # Optional
    #publishFeedCredentials: # Required when command == Push && NuGetFeedType == External
    #packagesToPack: '**/*.csproj' # Required when command == Pack
    #packDirectory: '$(Build.ArtifactStagingDirectory)' # Optional
    #nobuild: false # Optional
    #includesymbols: false # Optional
    #includesource: false # Optional
    #versioningScheme: 'off' # Options: off, byPrereleaseNumber, byEnvVar, byBuildNumber
    #versionEnvVar: # Required when versioningScheme == ByEnvVar
    #majorVersion: '1' # Required when versioningScheme == ByPrereleaseNumber
    #minorVersion: '0' # Required when versioningScheme == ByPrereleaseNumber
    #patchVersion: '0' # Required when versioningScheme == ByPrereleaseNumber
    #buildProperties: # Optional
    #verbosityPack: 'Detailed' # Options: -, quiet, minimal, normal, detailed, diagnostic
    workingDirectory:
```

### Transformed Github Action

```yaml
- run: dotnet build
```

### Unsupported Inputs

- publishWebProjects
- testRunTitle
- zipAfterPublish
- modifyOutputPath
- publishPackageMetadata
