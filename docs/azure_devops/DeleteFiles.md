# Delete Files Task

## Azure DevOps Input

```yaml
- task: DeleteFiles@1
  inputs:
    SourceFolder: 
    Contents: |
      tests
      *.json
    RemoveSourceFolder: false
    RemoveDotFiles: false
```

## Transformed Github Action

```yaml
- name: Delete files
  uses: actions/github-script@v6.4.0
  env:
    REMOVE_SOURCE_FOLDER: false
    SOURCE_FOLDER: "."
    FILE_PATTERNS: |-
      tests
      *.json
      !**/.*
  with:
    script: |-
      const fs = require('fs')
      const sourceFolder = process.env.SOURCE_FOLDER
      process.chdir(sourceFolder)
      const removeSourceFolder = process.env.REMOVE_SOURCE_FOLDER
      const globber = await glob.create(process.env.FILE_PATTERNS)
      const files = await globber.glob()
      files.sort().reverse()
      for (file of files) {
          console.log(`deleting ${file}`)
          await io.rmRF(file)
      }
      if (removeSourceFolder == "true" && sourceFolder !== ".") {
          if (fs.readdirSync(".").length === 0) {
              process.chdir("${{ github.workspace }}")
              console.log(`deleting ${sourceFolder}`)
              await io.rmRF(sourceFolder)
          }
      }
```

## Unsupported Inputs and Aliases
- Contents: Does not support entries that contain brace expansion and extglob style patterns
