# File Operations

## Designer Pipeline

### Jenkins Input

```xml
<builders>
    <sp.sd.fileoperations.FileOperationsBuilder plugin="file-operations@1.11">
        <fileOperations>
            <sp.sd.fileoperations.FileCreateOperation>
                <fileName>created.txt</fileName>
                <fileContent>testing=true name=foo num=1</fileContent>
            </sp.sd.fileoperations.FileCreateOperation>
            <sp.sd.fileoperations.FileCopyOperation>
                <includes>**/**.txt</includes>
                <excludes>**/**.js</excludes>
                <targetLocation>./new</targetLocation>
                <flattenFiles>true</flattenFiles>
                <renameFiles>false</renameFiles>
                <sourceCaptureExpression/>
                <targetNameExpression/>
            </sp.sd.fileoperations.FileCopyOperation>
            <sp.sd.fileoperations.FolderCopyOperation>
                <sourceFolderPath>new</sourceFolderPath>
                <destinationFolderPath>test/more_new</destinationFolderPath>
            </sp.sd.fileoperations.FolderCopyOperation>
            <sp.sd.fileoperations.FolderCreateOperation>
                <folderPath>foo/goo/zoo</folderPath>
            </sp.sd.fileoperations.FolderCreateOperation>
            <sp.sd.fileoperations.FileCreateOperation>
                <fileName>foo/goo/zoo/test.txt</fileName>
                <fileContent>testing is fun...</fileContent>
            </sp.sd.fileoperations.FileCreateOperation>
            <sp.sd.fileoperations.FileDeleteOperation>
                <includes>**/old/**</includes>
                <excludes>**/keep/**</excludes>
            </sp.sd.fileoperations.FileDeleteOperation>
            <sp.sd.fileoperations.FileJoinOperation>
                <sourceFile>created.txt</sourceFile>
                <targetFile>foo/goo/zoo/test.txt</targetFile>
            </sp.sd.fileoperations.FileJoinOperation>
            <sp.sd.fileoperations.FileRenameOperation>
                <source>foo/goo/zoo/test.txt</source>
                <destination>foo/goo/zoo/test2.txt</destination>
            </sp.sd.fileoperations.FileRenameOperation>
            <sp.sd.fileoperations.FolderRenameOperation>
                <source>foo/goo</source>
                <destination>foo/goo2</destination>
            </sp.sd.fileoperations.FolderRenameOperation>
            <sp.sd.fileoperations.FolderDeleteOperation>
                <folderPath>test/more_new</folderPath>
            </sp.sd.fileoperations.FolderDeleteOperation>
        </fileOperations>
    </sp.sd.fileoperations.FileOperationsBuilder>
    <hudson.tasks.Shell>
        <command>mkdir -p playground touch playground/my_file.txt tar zcf playground.tar playground</command>
        <configuredLocalRules/>
    </hudson.tasks.Shell>
    <sp.sd.fileoperations.FileOperationsBuilder plugin="file-operations@1.11">
        <fileOperations>
            <sp.sd.fileoperations.FileUnTarOperation>
                <filePath>playground.tar</filePath>
                <targetLocation>new</targetLocation>
                <isGZIP>true</isGZIP>
            </sp.sd.fileoperations.FileUnTarOperation>
            <sp.sd.fileoperations.FileZipOperation>
                <folderPath>foo/goo2</folderPath>
                <outputFolderPath>my_zip/dir1</outputFolderPath>
            </sp.sd.fileoperations.FileZipOperation>
            <sp.sd.fileoperations.FileUnZipOperation>
                <filePath>my_zip/dir1/goo2.zip</filePath>
                <targetLocation>output</targetLocation>
            </sp.sd.fileoperations.FileUnZipOperation>
            <sp.sd.fileoperations.FileTransformOperation>
                <includes>**/**.txt</includes>
                <excludes/>
            </sp.sd.fileoperations.FileTransformOperation>
        </fileOperations>
    </sp.sd.fileoperations.FileOperationsBuilder>
</builders>
```

### Transformed Github Action

```yaml
steps:
- name: checkout
  uses: actions/checkout@v2
- name: create file
  shell: bash
  run: |-
    cat > created.txt <<'EOL'
    testing=true
    name=foo
    num=1
    EOL
- name: copy files
  uses: actions/github-script@v6.4.0
  env:
    TARGET_LOCATION: "./new"
    FILE_PATTERNS: "**/**.txt,!**/**.js"
    FLATTEN: 'true'
  with:
    script: |-
      const fs = require('fs').promises
      const path = require('path')
      const target = path.resolve(process.env.TARGET_LOCATION)
      const patterns = process.env.FILE_PATTERNS
      const flatten = process.env.FLATTEN === 'true'
      const globber = await glob.create(patterns.replace(/,/g, "\n"))
      for await (const file of globber.globGenerator()) {
        if ((await fs.lstat(file)).isDirectory()) continue
        const filename = flatten ? path.basename(file) : file.substring(process.cwd().length)
        const dest = path.join(target, filename)
        await io.mkdirP(path.dirname(dest))
        await io.cp(file, dest)
      }
- name: copy folder
  shell: bash
  run: |-
    mkdir -p test/more_new
    cp -r new/* test/more_new
- name: create folder
  shell: bash
  run: mkdir -p foo/goo/zoo
- name: create file
  shell: bash
  run: |-
    mkdir -p foo/goo/zoo
    cat > foo/goo/zoo/test.txt <<'EOL'
    testing is fun...
    EOL
- name: delete files
  uses: actions/github-script@v6.4.0
  env:
    FILE_PATTERNS: "**/old/**,!**/keep/**"
  with:
    script: |-
      const fs = require('fs').promises
      const path = require('path')
      const patterns = process.env.FILE_PATTERNS
      const globber = await glob.create(patterns.replace(/,/g, "\n"))
      for await (const file of globber.globGenerator()) {
        if ((await fs.lstat(file)).isDirectory()) continue
        await io.rmRF(file)
      }
- name: join file
  shell: bash
  run: cat created.txt >> foo/goo/zoo/test.txt
- name: rename/move file
  shell: bash
  run: mv foo/goo/zoo/test.txt foo/goo/zoo/test2.txt
- name: rename/move folder
  shell: bash
  run: mv foo/goo foo/goo2
- name: delete folder
  shell: bash
  run: rm -rf test/more_new
- name: run command
  shell: bash
  run: |-
    mkdir -p playground
    touch playground/my_file.txt
    tar zcf playground.tar playground
- name: untar archive
  shell: bash
  run: |-
    mkdir -p new
    tar zxf playground.tar -C new
- name: zip folder
  shell: bash
  run: 7z a my_zip/dir1/goo2.zip ./foo/goo2/*
- name: unzip folder
  shell: bash
  run: unzip -d output my_zip/dir1/goo2.zip
#   # 'sp.sd.fileoperations.FileTransformOperation' was not transformed because there is no suitable equivalent in GitHub Actions
```

### Unsupported Operations
- File Transform
- File Properties To Json

### Unsupported Operations Options
- File Copy Operation
  - renameFiles
  - sourceCaptureExpression
  - targetNameExpression

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
    sh '''
        mkdir -p playground
        touch playground/my_file.txt
        tar cf playground.tar playground
    '''
    fileOperations([
        fileCreateOperation(fileName: 'created.txt', fileContent: "Hello World!" ),
        folderCreateOperation(folderPath: 'new'),
        fileCopyOperation(excludes: '**/**.js', flattenFiles: false, includes: '**/*.txt', targetLocation: './new'),
        folderCopyOperation(sourceFolderPath: "new", destinationFolderPath: "test/more_new"),
        folderCreateOperation(folderPath: "foo/goo/zoo"),
        fileCreateOperation(fileName: 'foo/goo/zoo/test.txt', fileContent: "testing is fun..." ),
        folderCreateOperation(folderPath: "old"),
        fileCreateOperation(fileName: 'old/created.txt', fileContent: "Hello World!" ),
        fileDeleteOperation(includes: "**/old/**", excludes: "**/keep/**"),
        fileRenameOperation(source: "foo/goo/zoo/test.txt", destination: "foo/goo/zoo/test2.txt"),
        folderDeleteOperation(folderPath: 'test/more_new'),
        folderRenameOperation(source: "foo/goo", destination: "foo/goo2"),
        folderDeleteOperation(folderPath: 'test/more_new'),
        fileUnTarOperation(filePath: 'playground.tar', targetLocation: "new" ),
        fileZipOperation(folderPath: "foo/goo2", outputFolderPath: "my_zip/dir1" ),
        fileUnZipOperation(filePath: "my_zip/dir1/goo2.zip", targetLocation: "output" ),
        fileTransformOperation(includes: "", excludes: ""),
        fileDownloadOperation(
            url: "https://httpbin.org/image",
            userName: "",
            password: "",
            targetLocation: ".",
            targetFileName: "image.webp"
        )
    ])
}
```

### Transformed Github Action

```yaml
steps:
- name: checkout
  uses: actions/checkout@v2
- name: sh
  shell: bash
  run: |-
    mkdir -p playground
    touch playground/my_file.txt
    tar cf playground.tar playground
- name: create file
  shell: bash
  run: |-
    cat > created.txt <<'EOL'
    Hello World!
    EOL
- name: create folder
  shell: bash
  run: mkdir -p new
- name: copy files
  uses: actions/github-script@v6.4.0
  env:
    TARGET_LOCATION: "./new"
    FILE_PATTERNS: "**/*.txt,!**/**.js"
    FLATTEN: false
  with:
    script: |-
      const fs = require('fs').promises
      const path = require('path')
      const target = path.resolve(process.env.TARGET_LOCATION)
      const patterns = process.env.FILE_PATTERNS
      const flatten = process.env.FLATTEN === 'true'
      const globber = await glob.create(patterns.replace(/,/g, "\n"))
      for await (const file of globber.globGenerator()) {
        if ((await fs.lstat(file)).isDirectory()) continue
        const filename = flatten ? path.basename(file) : file.substring(process.cwd().length)
        const dest = path.join(target, filename)
        await io.mkdirP(path.dirname(dest))
        await io.cp(file, dest)
      }
- name: copy folder
  shell: bash
  run: |-
    mkdir -p test/more_new
    cp -r new/* test/more_new
- name: create folder
  shell: bash
  run: mkdir -p foo/goo/zoo
- name: create file
  shell: bash
  run: |-
    mkdir -p foo/goo/zoo
    cat > foo/goo/zoo/test.txt <<'EOL'
    testing is fun...
    EOL
- name: create folder
  shell: bash
  run: mkdir -p old
- name: create file
  shell: bash
  run: |-
    mkdir -p old
    cat > old/created.txt <<'EOL'
    Hello World!
    EOL
- name: delete files
  uses: actions/github-script@v6.4.0
  env:
    FILE_PATTERNS: "**/old/**,!**/keep/**"
  with:
    script: |-
      const fs = require('fs').promises
      const path = require('path')
      const patterns = process.env.FILE_PATTERNS
      const globber = await glob.create(patterns.replace(/,/g, "\n"))
      for await (const file of globber.globGenerator()) {
        if ((await fs.lstat(file)).isDirectory()) continue
        await io.rmRF(file)
      }
- name: rename/move file
  shell: bash
  run: mv foo/goo/zoo/test.txt foo/goo/zoo/test2.txt
- name: delete folder
  shell: bash
  run: rm -rf test/more_new
- name: rename/move folder
  shell: bash
  run: mv foo/goo foo/goo2
- name: delete folder
  shell: bash
  run: rm -rf test/more_new
- name: untar archive
  shell: bash
  run: |-
    mkdir -p new
    tar xf playground.tar -C new
- name: zip folder
  shell: bash
  run: 7z a my_zip/dir1/goo2.zip ./foo/goo2
- name: unzip folder
  shell: bash
  run: unzip -d output my_zip/dir1/goo2.zip
#   # 'fileTransformOperation' was not transformed because there is no suitable equivalent in GitHub Actions
- name: download file
  shell: bash
  run: curl https://httpbin.org/image --output ./image.webp
```

### Unsupported Operations
- fileTransformOperation
- filePropertiesToJsonOperation

### Unsupported Operations Options
- fileCopyOperation
  - renameFiles
  - sourceCaptureExpression
  - targetNameExpression
