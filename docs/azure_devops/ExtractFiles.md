# Extract Files Task

## Azure DevOps Input

```yaml
- task: ExtractFiles@1
  inputs:
    archiveFilePatterns: '**/*.zip'                     # Required
    destinationFolder: 'base_folder'                    # Required, Default: $(Build.SourcesDirectory)
    cleanDestinationFolder: true                        # Required, Default: true
    overwriteExistingFiles: true                        # Required, Default: false
    pathToSevenZipTool: 'path-to-7z-utility'            # Optional, 
```

## Transformed Github Action

```yaml
- run: npm i @actions/exec
- name: Extract Files
  uses: actions/github-script@v6.4.0
  env:
    DESTINATION_FOLDER: base_folder
    ARCHIVE_FILE_PATTERNS: "**/*.zip"
  with:
    script: |-
      const fs = require('fs').promises
      const path = require('path')
      const exec = require('@actions/exec')

      const target = path.resolve(process.env.DESTINATION_FOLDER)
      const patterns = process.env.ARCHIVE_FILE_PATTERNS
      const globber = await glob.create(patterns)
      await io.mkdirP(path.dirname(target))

      for await (const file of globber.globGenerator()) {
        if ((await fs.lstat(file)).isDirectory()) continue
        await exec.exec(`7z x ${file} -o${target} -aoa`)
      }
```

## Unsupported Inputs and Aliases
- pathToSevenZipTool
