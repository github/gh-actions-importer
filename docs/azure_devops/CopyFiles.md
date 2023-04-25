# CopyFiles task

## Azure DevOps Input

```yaml
# Copy files
# Copy files from a source folder to a target folder using patterns matching file paths (not folder paths)
- task: CopyFiles@2
  inputs:
    #sourceFolder: # Optional
    #contents: '**'
    targetFolder:
    #cleanTargetFolder: false # Optional
    #overWrite: false # Optional
    #flattenFolders: false # Optional
    #preserveTimestamp: false # Optional
    #retryCount: 0 # Optional
```

### Transformed Github Action

```yaml
    # The following script preserves the globbing behavior of the CopyFiles task.
    # Refer to this transformer's documentation for an alternative that will work in simple cases.
    - uses: actions/github-script@v6.4.0
      env:
        TARGET_FOLDER: dest
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
```

### Unsupported Inputs

- preserveTimestamp
- retryCount

### Alternative transformation

The above script might be overly complicated for simple cases where extended blob syntax is not used. Use the following custom transformer for a more idiomatic result (when possible).

```ruby
transform "CopyFiles@2" do |item|
  contents = item["contents"] || "**"
  source = item["sourceFolder"] || "."
  target = item["targetFolder"]

  script = []

  script << "rm -rf #{target}/*" if item["cleanTargetFolder"] == true
  script << "mkdir -p #{target}"

  contents.split("\n").each do |pattern|
    script << "cp #{source}/#{pattern} #{target}"
  end

  {
    name: "Copy files",
    run: script.join("\n")
  }
end
```
