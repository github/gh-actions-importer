# Copy Artifact

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.copyartifact.CopyArtifact plugin="copyartifact@1.45.1">
    <project>github/artifact</project>
    <filter>**/dir2/**</filter>
    <target>moved</target>
    <excludes/>
    <selector class="hudson.plugins.copyartifact.StatusBuildSelector"/>
    <doNotFingerprintArtifacts>true</doNotFingerprintArtifacts>
</hudson.plugins.copyartifact.CopyArtifact>
```

### Transformed Github Action

```yaml
- name: download artifact
  uses: dawidd6/action-download-artifact@v2.26.0
  with:
    github_token: "${{ secrets.GITHUB_TOKEN }}"
    workflow: "${{ env.WORKFLOW_NAME }}"
    workflow_conclusion: "${{ env.CONCLUSION }}"
    name: "${{ env.ARTIFACT_NAME }}"
    path: "${{ env.TARGET_PATH }}"
    repo: "${{ env.REPO }}"
  env:
    WORKFLOW_NAME: UPDATE_ME
    CONCLUSION: completed,success
    TARGET_PATH: moved
    ARTIFACT_NAME: UPDATE_ME
    REPO: "${{github.repository}}"
  continue-on-error: false
```

### Unsupported Options
- Stable build only
- Artifacts to copy (Does not support globbing patterns)
- Artifacts not to copy
- Parameter filters
- Flatten directories
- Fingerprint Artifacts
- Which build:
  - Latest saved build (marked "keep forever")
  - Upstream build that triggered this job
  - Build triggered by current MultiJob build
  - Downstream build of
  - Last build with artifacts
  - Specified by permalink
  - Copy from WORKSPACE of latest completed build
  - Specified by a build parameter
  - Result variable suffix

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
    copyArtifacts filter: '**/dir1/**', fingerprintArtifacts: true, optional: true, projectName: 'github/test-artifact', selector: lastSuccessful(), target: 'moved'
}
```

### Transformed Github Action

```yaml
- name: download artifact
  uses: dawidd6/action-download-artifact@v2.26.0
  with:
    github_token: "${{ secrets.GITHUB_TOKEN }}"
    workflow: "${{ env.WORKFLOW_NAME }}"
    workflow_conclusion: "${{ env.CONCLUSION }}"
    name: "${{ env.ARTIFACT_NAME }}"
    path: "${{ env.TARGET_PATH }}"
    repo: "${{ env.REPO }}"
  env:
    WORKFLOW_NAME: UPDATE_ME
    CONCLUSION: completed,success
    TARGET_PATH: moved
    ARTIFACT_NAME: UPDATE_ME
    REPO: "${{github.repository}}"
  continue-on-error: true
```

### Unsupported Options
- excludes
- filter
- fingerprintArtifact
- flatten
- parameters
- resultVariableSuffix
- following selectors
  - downstream
  - lastWithArtifacts
  - $class: 'MultiJobBuildSelector'
  - buildParameter
  - permalink
  - $class: 'PromotedBuildSelector'
  - latestSavedBuild
  - upstream
  - workspace
