# Archive Artifacts

## Designer Pipeline

### Jenkins Input

```xml
<hudson.tasks.ArtifactArchiver>
  <artifacts>archive/path/</artifacts>
  <excludes>exclude/path/</excludes>
  <allowEmptyArchive>true</allowEmptyArchive>
  <onlyIfSuccessful>true</onlyIfSuccessful>
  <fingerprint>true</fingerprint>
  <defaultExcludes>true</defaultExcludes>
  <caseSensitive>true</caseSensitive>
  <followSymlinks>true</followSymlinks>
</hudson.tasks.ArtifactArchiver>
```

### Transformed Github Action

```yaml
 - name: Upload Artifacts
      uses: actions/upload-artifact@v2
      with:
        if-no-files-found: ignore
        path: |-
          archive/path/
          !exclude/path/
          !**/*~
          !**/#*#
          !**/.#*
          !**/%*%
          !**/._*
          !**/CVS
          !**/CVS/**
          !**/.cvsignore
          !**/SCCS
          !**/SCCS/**
          !**/vssver.scc
          !**/.svn
          !**/.svn/**
          !**/.DS_Store
          !**/.git
          !**/.git/**
          !**/.gitattributes
          !**/.gitignore
          !**/.gitmodules
          !**/.hg
          !**/.hg/**
          !**/.hgignore
          !**/.hgsub
          !**/.hgsubstate
          !**/.hgtags
          !**/.bzr
          !**/.bzr/**
          !**/.bzrignore
      if: success()
```

### Supported Options

- Artifact paths (artifact)
- Exclude paths (excludes)
- Allow empty archive (allowEmptyArchive)
- Run if successful run only (onlyIfSuccessful)
- Default ant exclude paths (defaultExcludes)

### Unsupported Options

- Case sensitive (caseSensitive)
- Fingerprint (fingerprint)
- Follow system links (followSymlinks)

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
  archiveArtifacts artifacts: 'build/libs/**/*.jar', fingerprint: false, allowEmptyArchive: false, caseSensitive: false, defaultExcludes: false, excludes: false, onlyIfSuccessful: false
  archiveArtifacts 'build/libs/**/*.jar'
  }
```

### Transformed Github Action

```yaml
 - name: Upload Artifacts
      uses: actions/upload-artifact@v2
      with:
        if-no-files-found: ignore
        path: |-
          archive/path/
          !exclude/path/
          !**/*~
          !**/#*#
          !**/.#*
          !**/%*%
          !**/._*
          !**/CVS
          !**/CVS/**
          !**/.cvsignore
          !**/SCCS
          !**/SCCS/**
          !**/vssver.scc
          !**/.svn
          !**/.svn/**
          !**/.DS_Store
          !**/.git
          !**/.git/**
          !**/.gitattributes
          !**/.gitignore
          !**/.gitmodules
          !**/.hg
          !**/.hg/**
          !**/.hgignore
          !**/.hgsub
          !**/.hgsubstate
          !**/.hgtags
          !**/.bzr
          !**/.bzr/**
          !**/.bzrignore
      if: success()
```

### Unsupported Options

- Case sensitive (caseSensitive)
- Fingerprint (fingerprint)
