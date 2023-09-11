# SCP Publish

## Designer Pipeline

### Jenkins Input

```xml
<publishers>
    <be.certipost.hudson.plugin.SCPRepositoryPublisher plugin="scp@1.8">
        <siteName>127.0.0.1</siteName>
        <entries>
            <be.certipost.hudson.plugin.Entry>
                <filePath>/results</filePath>
                <sourceFile>**/**.txt</sourceFile>
                <keepHierarchy>false</keepHierarchy>
            </be.certipost.hudson.plugin.Entry>
        </entries>
    </be.certipost.hudson.plugin.SCPRepositoryPublisher>
</publishers>
```

### Transformed Github Action

```yaml
    name: Install SSH key
    uses: shimataro/ssh-key-action@v2.5.0
    with:
      key: "${{ secrets.SCP_127_0_0_1_SSH_KEY }}"
      name: id_rsa-SCP_127_0_0_1
      known_hosts: "${{ secrets.SCP_127_0_0_1_KNOWN_HOSTS }}"
      if_key_exists: fail
      config: |
        Host 127.0.0.1
          HostName 127.0.0.1
          User ${{ secrets.SCP_127_0_0_1_USER }}
          IdentityFile ~/.ssh/id_rsa-SCP_127_0_0_1
    name: setup file transfer file
    uses: actions/github-script@v6.4.0
    with:
      script: |-
        const fs = require('fs').promises
        const path = require('path')
        const patterns = "**/**.txt"
        const globber = await glob.create(patterns.replace(",", "\n"))
        const files = []
        for await (const file of globber.globGenerator()) {
            if ((await fs.lstat(file)).isDirectory()) continue
            files.push(path.relative(process.cwd(), file))
        }
        fs.writeFile("scp_transfer.txt", files.join("\n"), (err) => {})
    name: run file transfers
    run: |-
      ssh 127.0.0.1 'mkdir -p /results'
      tar -cvf scp_transfer.tar --files-from scp_transfer.txt
      scp scp_transfer.tar 127.0.0.1:
      ssh 127.0.0.1 'tar -xvf scp_transfer.tar -C /results && rm scp_transfer.tar'
```

### Unsupported Options

- Keeps Hierarchy (currrently always keeps folder structure)
