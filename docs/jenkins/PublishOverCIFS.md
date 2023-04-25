# Publish Over CIFS

## Designer Pipeline

### Jenkins Input

#### Builder
```xml
<jenkins.plugins.publish__over__cifs.CifsBuilderPlugin plugin="publish-over-cifs@0.16">
    <delegate>
        <consolePrefix>CIFS:</consolePrefix>
        <delegate plugin="publish-over@0.22">
            <publishers>
                <jenkins.plugins.publish__over__cifs.CifsPublisher plugin="publish-over-cifs@0.16">
                    <configName>jd</configName>
                    <verbose>false</verbose>
                    <transfers>
                        <jenkins.plugins.publish__over__cifs.CifsTransfer>
                            <remoteDirectory/>
                            <sourceFiles>**/*.txt</sourceFiles>
                            <excludes>**/test/**</excludes>
                            <removePrefix/>
                            <remoteDirectorySDF>false</remoteDirectorySDF>
                            <flatten>false</flatten>
                            <cleanRemote>false</cleanRemote>
                            <noDefaultExcludes>true</noDefaultExcludes>
                            <makeEmptyDirs>false</makeEmptyDirs>
                            <patternSeparator>[, ]+</patternSeparator>
                        </jenkins.plugins.publish__over__cifs.CifsTransfer>
                    </transfers>
                    <useWorkspaceInPromotion>false</useWorkspaceInPromotion>
                    <usePromotionTimestamp>false</usePromotionTimestamp>
                </jenkins.plugins.publish__over__cifs.CifsPublisher>
            </publishers>
            <continueOnError>false</continueOnError>
            <failOnError>false</failOnError>
            <alwaysPublishFromMaster>false</alwaysPublishFromMaster>
            <hostConfigurationAccess class="jenkins.plugins.publish_over_cifs.CifsPublisherPlugin" reference="../.."/>
        </delegate>
        <publishWhenFailed>false</publishWhenFailed>
    </delegate>
</jenkins.plugins.publish__over__cifs.CifsBuilderPlugin>
```

#### Publisher (Post Build)
```xml
<jenkins.plugins.publish__over__cifs.CifsPublisherPlugin plugin="publish-over-cifs@0.16">
    <consolePrefix>CIFS:</consolePrefix>
    <delegate plugin="publish-over@0.22">
        <publishers>
            <jenkins.plugins.publish__over__cifs.CifsPublisher plugin="publish-over-cifs@0.16">
                <configName>jd</configName>
                <verbose>false</verbose>
                <transfers>
                    <jenkins.plugins.publish__over__cifs.CifsTransfer>
                        <remoteDirectory>foo</remoteDirectory>
                        <sourceFiles>**/*.txt</sourceFiles>
                        <excludes/>
                        <removePrefix/>
                        <remoteDirectorySDF>false</remoteDirectorySDF>
                        <flatten>false</flatten>
                        <cleanRemote>false</cleanRemote>
                        <noDefaultExcludes>true</noDefaultExcludes>
                        <makeEmptyDirs>false</makeEmptyDirs>
                        <patternSeparator>[, ]+</patternSeparator>
                    </jenkins.plugins.publish__over__cifs.CifsTransfer>
                </transfers>
                <useWorkspaceInPromotion>false</useWorkspaceInPromotion>
                <usePromotionTimestamp>false</usePromotionTimestamp>
            </jenkins.plugins.publish__over__cifs.CifsPublisher>
        </publishers>
        <continueOnError>false</continueOnError>
        <failOnError>false</failOnError>
        <alwaysPublishFromMaster>false</alwaysPublishFromMaster>
        <hostConfigurationAccess class="jenkins.plugins.publish_over_cifs.CifsPublisherPlugin" reference="../.."/>
    </delegate>
    <publishWhenFailed>false</publishWhenFailed>
</jenkins.plugins.publish__over__cifs.CifsPublisherPlugin>
```

### Transformed Github Action for Builder

```yaml
- name: transfer files over CIFS
  run: |-
    sudo mkdir -p /mnt/cifs_share
    sudo mount -t cifs -o username=${{ env.JD_USER }},password=${{ secrets.JD_PASSWORD }} //${{ env.JD_HOST }}/${{ env.JD_SHARE_DIR }} /mnt/cifs_share
    sudo rsync -a --prune-empty-dirs --exclude='**/test/**' --include='**/*.txt' --include='*/' --exclude='*' ./ /mnt/cifs_share/
    sudo umount /mnt/cifs_share
  env:
    JD_SHARE_DIR: UPDATE_ME
    JD_HOST: UPDATE_ME
    JD_USER: UPDATE_ME
```

### Transformed Github Action for Publisher

```yaml
- name: transfer files over CIFS
  run: |-
    sudo mkdir -p /mnt/cifs_share
    sudo mount -t cifs -o username=${{ env.JD_USER }},password=${{ secrets.JD_PASSWORD }} //${{ env.JD_HOST }}/${{ env.JD_SHARE_DIR }} /mnt/cifs_share
    sudo mkdir -p /mnt/cifs_share/foo
    sudo rsync -a --prune-empty-dirs --include='**/*.txt' --include='*/' --exclude='*' ./ /mnt/cifs_share/foo
    sudo umount /mnt/cifs_share
  env:
    JD_SHARE_DIR: UPDATE_ME
    JD_HOST: UPDATE_ME
    JD_USER: UPDATE_ME
  if: always()
```

### Unsupported Options
- Pattern separator
- Flatten files
- Remote directory is a date format
- Clean remote
