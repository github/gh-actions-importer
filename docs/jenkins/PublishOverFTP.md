# Publish Over FTP

## Designer Pipeline

### Jenkins Input

#### Builder
```xml
<jenkins.plugins.publish__over__ftp.BapFtpBuilder plugin="publish-over-ftp@1.16">
    <delegate>
        <consolePrefix>FTP:</consolePrefix>
        <delegate plugin="publish-over@0.22">
            <publishers>
                <jenkins.plugins.publish__over__ftp.BapFtpPublisher plugin="publish-over-ftp@1.16">
                    <configName>FTP_Server</configName>
                    <verbose>false</verbose>
                    <transfers>
                        <jenkins.plugins.publish__over__ftp.BapFtpTransfer>
                            <remoteDirectory>new</remoteDirectory>
                            <sourceFiles>**/**.txt</sourceFiles>
                            <excludes/>
                            <removePrefix/>
                            <remoteDirectorySDF>false</remoteDirectorySDF>
                            <flatten>false</flatten>
                            <cleanRemote>false</cleanRemote>
                            <noDefaultExcludes>true</noDefaultExcludes>
                            <makeEmptyDirs>false</makeEmptyDirs>
                            <patternSeparator>[, ]+</patternSeparator>
                            <asciiMode>false</asciiMode>
                        </jenkins.plugins.publish__over__ftp.BapFtpTransfer>
                    </transfers>
                    <useWorkspaceInPromotion>false</useWorkspaceInPromotion>
                    <usePromotionTimestamp>false</usePromotionTimestamp>
                </jenkins.plugins.publish__over__ftp.BapFtpPublisher>
            </publishers>
            <continueOnError>false</continueOnError>
            <failOnError>false</failOnError>
            <alwaysPublishFromMaster>false</alwaysPublishFromMaster>
            <hostConfigurationAccess class="jenkins.plugins.publish_over_ftp.BapFtpPublisherPlugin" reference="../.."/>
        </delegate>
    </delegate>
</jenkins.plugins.publish__over__ftp.BapFtpBuilder>
```

#### Publisher (Post Build)
```xml
<jenkins.plugins.publish__over__ftp.BapFtpPublisherPlugin plugin="publish-over-ftp@1.16">
    <consolePrefix>FTP:</consolePrefix>
    <delegate plugin="publish-over@0.22">
        <publishers>
            <jenkins.plugins.publish__over__ftp.BapFtpPublisher plugin="publish-over-ftp@1.16">
                <configName>FTP_Server</configName>
                <verbose>false</verbose>
                <transfers>
                    <jenkins.plugins.publish__over__ftp.BapFtpTransfer>
                        <remoteDirectory/>
                        <sourceFiles>**/**.txt</sourceFiles>
                        <excludes>**/**.json</excludes>
                        <removePrefix/>
                        <remoteDirectorySDF>false</remoteDirectorySDF>
                        <flatten>false</flatten>
                        <cleanRemote>false</cleanRemote>
                        <noDefaultExcludes>false</noDefaultExcludes>
                        <makeEmptyDirs>false</makeEmptyDirs>
                        <patternSeparator>[, ]+</patternSeparator>
                        <asciiMode>false</asciiMode>
                    </jenkins.plugins.publish__over__ftp.BapFtpTransfer>
                </transfers>
                <useWorkspaceInPromotion>false</useWorkspaceInPromotion>
                <usePromotionTimestamp>false</usePromotionTimestamp>
            </jenkins.plugins.publish__over__ftp.BapFtpPublisher>
        </publishers>
        <continueOnError>false</continueOnError>
        <failOnError>false</failOnError>
        <alwaysPublishFromMaster>false</alwaysPublishFromMaster>
        <hostConfigurationAccess class="jenkins.plugins.publish_over_ftp.BapFtpPublisherPlugin" reference="../.."/>
    </delegate>
</jenkins.plugins.publish__over__ftp.BapFtpPublisherPlugin>
```

### Transformed Github Action

```yaml
steps:
    - name: setup ftp transfer file
      uses: actions/github-script@v6.4.0
      with:
        script: |-
          const fs = require('fs').promises
          const path = require('path')
          const patterns = "**/**.txt"
          const globber = await glob.create(patterns.replace(",", "\n"))
          const files = []
          const dirs = []
          const ignore_dirs = ['./', '.\\']
          const isAsciiMode = "true"
          for await (let file of globber.globGenerator()) {
            file = path.relative(process.cwd(), file)
            if ((await fs.lstat(file)).isDirectory()) continue
              path.dirname(file).split(path.sep).reduce((prevPath, folder) => {
              const currentPath = path.join(prevPath, folder, path.sep);
              if (!ignore_dirs.includes(currentPath)) {
                dirs.push("mkdir " + currentPath);
              }
              return currentPath;
            }, '');
            files.push("put " + file)
          }
          uniq_dirs = [...new Set(dirs)];
          uniq_dirs.sort((a, b) => {
            return a.length - b.length;
          });
          var ftp_commands = [
            "open ${{ secrets.FTP_SERVER_HOST }} ${{ env.FTP_PORT }}",
            "user ${{ secrets.FTP_SERVER_USER }} ${{ secrets.FTP_SERVER_PASSWORD }}",
            "mkdir new",
            "cd new"
          ]
          if (isAsciiMode === "false") {
            ftp_commands.push("binary")
          }
          ftp_commands = ftp_commands.concat(uniq_dirs.concat(files))
          fs.writeFile("FTP_SERVER_transfer.txt", ftp_commands.join("\n"), (err) => { })
      env:
        FTP_PORT: 21
    - name: run file transfers
      run: ftp -pvn < FTP_SERVER_transfer.txt && rm FTP_SERVER_transfer.txt
```

### Unsupported Options
- Remove prefix
- Pattern separator
- Make empty dirs
- Flatten files
- Remote directory is a date format
- Clean remote
