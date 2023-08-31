# Publish Over SSH

## Designer Pipeline

### Jenkins Input

#### Builder
```xml
<jenkins.plugins.publish__over__ssh.BapSshBuilderPlugin plugin="publish-over-ssh@1.22">
    <delegate>
        <consolePrefix>SSH:</consolePrefix>
        <delegate plugin="publish-over@0.22">
            <publishers>
                <jenkins.plugins.publish__over__ssh.BapSshPublisher plugin="publish-over-ssh@1.22">
                    <configName>Server1</configName>
                    <verbose>true</verbose>
                    <transfers>
                        <jenkins.plugins.publish__over__ssh.BapSshTransfer>
                            <remoteDirectory>goo/foo</remoteDirectory>
                            <sourceFiles>*.txt,new/test/*.json</sourceFiles>
                            <excludes/>
                            <removePrefix/>
                            <remoteDirectorySDF>true</remoteDirectorySDF>
                            <flatten>false</flatten>
                            <cleanRemote>false</cleanRemote>
                            <noDefaultExcludes>true</noDefaultExcludes>
                            <makeEmptyDirs>false</makeEmptyDirs>
                            <patternSeparator>[, ]+</patternSeparator>
                            <execCommand>echo "Hi from Jenkins $(date)" > remote_server.txt
                            </execCommand>
                            <execTimeout>120000</execTimeout>
                            <usePty>false</usePty>
                            <useAgentForwarding>false</useAgentForwarding>
                            <useSftpForExec>false</useSftpForExec>
                        </jenkins.plugins.publish__over__ssh.BapSshTransfer>
                    </transfers>
                    <useWorkspaceInPromotion>false</useWorkspaceInPromotion>
                    <usePromotionTimestamp>false</usePromotionTimestamp>
                </jenkins.plugins.publish__over__ssh.BapSshPublisher>
            </publishers>
            <continueOnError>false</continueOnError>
            <failOnError>false</failOnError>
            <alwaysPublishFromMaster>false</alwaysPublishFromMaster>
            <hostConfigurationAccess class="jenkins.plugins.publish_over_ssh.BapSshPublisherPlugin" reference="../.."/>
        </delegate>
    </delegate>
</jenkins.plugins.publish__over__ssh.BapSshBuilderPlugin>
```

#### Publisher (Post Build)
```xml
<jenkins.plugins.publish__over__ssh.BapSshPublisherPlugin plugin="publish-over-ssh@1.22">
    <consolePrefix>SSH:</consolePrefix>
    <delegate plugin="publish-over@0.22">
        <publishers>
            <jenkins.plugins.publish__over__ssh.BapSshPublisher plugin="publish-over-ssh@1.22">
                <configName>Server1</configName>
                <verbose>false</verbose>
                <transfers>
                    <jenkins.plugins.publish__over__ssh.BapSshTransfer>
                        <remoteDirectory>goo/foo</remoteDirectory>
                        <sourceFiles>*.txt,new/test/*.json</sourceFiles>
                        <excludes/>
                        <removePrefix/>
                        <remoteDirectorySDF>false</remoteDirectorySDF>
                        <flatten>false</flatten>
                        <cleanRemote>false</cleanRemote>
                        <noDefaultExcludes>true</noDefaultExcludes>
                        <makeEmptyDirs>false</makeEmptyDirs>
                        <patternSeparator>[, ]+</patternSeparator>
                        <execCommand>echo "Hi from Jenkins $(date)" > remote_server.txt echo "Hello Again!"
                        </execCommand>
                        <execTimeout>120000</execTimeout>
                        <usePty>false</usePty>
                        <useAgentForwarding>false</useAgentForwarding>
                        <useSftpForExec>false</useSftpForExec>
                    </jenkins.plugins.publish__over__ssh.BapSshTransfer>
                </transfers>
                <useWorkspaceInPromotion>false</useWorkspaceInPromotion>
                <usePromotionTimestamp>false</usePromotionTimestamp>
            </jenkins.plugins.publish__over__ssh.BapSshPublisher>
        </publishers>
        <continueOnError>false</continueOnError>
        <failOnError>false</failOnError>
        <alwaysPublishFromMaster>false</alwaysPublishFromMaster>
        <hostConfigurationAccess class="jenkins.plugins.publish_over_ssh.BapSshPublisherPlugin" reference="../.."/>
    </delegate>
</jenkins.plugins.publish__over__ssh.BapSshPublisherPlugin>
```
### Transformed Github Action

```yaml
    steps:
    - name: checkout
      uses: actions/checkout@v2
    # Ensure parameter if_key_exists is set correctly
    - name: Install SSH key
      uses: shimataro/ssh-key-action@v2.5.0
      with:
        key: "${{ secrets.SERVER1_SSH_KEY }}"
        name: id_rsa-server1
        known_hosts: "${{ secrets.SERVER1_KNOWN_HOSTS }}"
        if_key_exists: fail
        config: |
          Host SERVER1
            HostName ${{ secrets.SERVER1_HOST_NAME }}
            User ${{ secrets.SERVER1_USER }}
            IdentityFile ~/.ssh/id_rsa-server1
    - name: setup file tranfer file
      uses: actions/github-script@v6.4.0
      with:
        script: |-
          const fs = require('fs').promises
          const path = require('path')
          const patterns = "*.txt,new/test/*.json"
          const globber = await glob.create(patterns.replace(",", "\n"))
          const files = []
          for await (const file of globber.globGenerator()) {
              if ((await fs.lstat(file)).isDirectory()) continue
              files.push(path.relative(process.cwd(), file))
          }
          fs.writeFile("server1-transfer.txt", files.join("\n"), (err) => {})
    - name: run file transfers
      run: |-
        ssh SERVER1 'mkdir -p goo/foo'
        tar -cvf server1-transfer.tar --files-from server1-transfer.txt
        scp server1-transfer.tar SERVER1:
        ssh SERVER1 'tar -xvf server1-transfer.tar -C goo/foo && rm server1-transfer.tar'
    - name: run commands over ssh
      run: ssh SERVER1 'echo "Hi from Jenkins $(date)" > remote_server.txt'


```
### Unsupported Options

- Remove prefix
- Pattern separator
- Make empty dirs
- Flatten files
- Clean remote
- Remote directory is a date format
- Exec in pty
- Exec using Agent Forwarding
- Exec timeout (ms)
