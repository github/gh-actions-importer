# Copy Publish Build Artifacts Task

## Azure DevOps Input

```yaml
task: CopyPublishBuildArtifacts@1
  inputs:
    CopyRoot: 'SampleAzureFunction'
    Contents: '**\\bin\\Release'
    ArtifactName: 'SampleAzureFunction'
    ArtifactType: 'Container'
```

## Transformed Github Action

```yaml
    # The following script preserves the globbing behavior of the CopyFiles task.
    - name: 'Copy Publish Artifact: SampleAzureFunction'
      uses: actions/github-script@v6.4.0
      env:
        TARGET_FOLDER: ${{ runner.temp }}/${{ github.run_id }}_publishartifact
        SOURCE_FOLDER: SampleAzureFunction
        CONTENTS: "**\\bin\\Release"
      with:
        github-token: "${{ secrets.GITHUB_TOKEN }}"
        script: |-
          const fs = require('fs').promises
          const path = require('path')
          const target = path.resolve(process.env.TARGET_FOLDER)
          process.chdir(process.env.SOURCE_FOLDER || '.')
          if (process.env.CLEAN_TARGET_FOLDER === 'true') await io.rmRF(target)
          const flattenFolders = process.env.FLATTEN_FOLDERS === 'true'
          const options = {force: process.env.OVERWRITE === 'true'}
          const globber = await glob.create(process.env.CONTENTS || '**')
          for await (const file of globber.globGenerator()) {
            if ((await fs.lstat(file)).isDirectory()) continue
            const filename = flattenFolders ? path.basename(file) : file.substring(process.cwd().length)
            const dest = path.join(target, filename)
            await io.mkdirP(path.dirname(dest))
            await io.cp(file, dest, options)
          }
    - name: 'Copy Publish Artifact: SampleAzureFunction'
      uses: actions/upload-artifact@v2
      with:
        name: SampleAzureFunction
        path: "${{ runner.temp }}/${{ github.run_id }}_publishartifact"
```

## Unsupported Inputs and Aliases
- artifactType
- fileSharePath
