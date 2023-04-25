# Artifact Deployer

## Designer Pipeline

### Jenkins Input

```xml
{
  "plugin"=>"artifactdeployer@1.2", 
  "entries"=>[
    {
      "org.jenkinsci.plugins.artifactdeployer.ArtifactDeployerEntry"=>{
        "includes"=>"*/*.*", 
        "basedir"=>nil, 
        "excludes"=>nil, 
        "remote"=>"/nfs/build/deploy/${BUILD_ID}", 
        "flatten"=>"false", 
        "deleteRemote"=>"false", 
        "deleteRemoteArtifacts"=>"false", 
        "failNoFilesDeploy"=>"false"
        }
      }
    ], 
  "deployEvenBuildFail"=>"false"
  }
```

### Transformed Github Action

```yaml  
    - name: Artifacts Deploy
      uses: actions/upload-artifact@v2
      with:
        path: "${{ github.workspace }}"
      if: always()
```

### Unsupported Options

- remote
- flatten
- deleteRemote
- deleteRemoteArtifacts

### Retention Period

Artifacts are retained for 90 days by default. We can specify a shorter retention period using the retention-days input: and the retention period value must be between 1 and 90 inclusive.
