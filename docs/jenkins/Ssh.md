# SSH

## Designer Pipeline

### Jenkins Input

```xml
<buildWrappers>
    <org.jvnet.hudson.plugins.SSHBuildWrapper plugin="ssh@2.6.1">
        <siteName>user@127.0.0.1:22</siteName>
        <preScript>echo "before build script"</preScript>
        <postScript>echo "after build script"</postScript>
        <hideCommand>true</hideCommand>
    </org.jvnet.hudson.plugins.SSHBuildWrapper>
</buildWrappers>
```

### Transformed Github Action

```yaml
steps:
# Ensure parameter if_key_exists is set correctly
- name: Install SSH key
  uses: shimataro/ssh-key-action@v2.5.0
  with:
    key: "${{ secrets.127_0_0_1_SSH_KEY }}"
    name: id_rsa
    known_hosts: "${{ secrets.127_0_0_1_KNOWN_HOSTS }}"
    if_key_exists: fail
- name: checkout
  uses: actions/checkout@v2
- name: run script over ssh
  run: |-
    ssh -T user@127.0.0.1 -p 22 <<'EOL'
    echo "before build script"
    EOL
- name: run script over ssh
  run: |-
    ssh -T user@127.0.0.1 -p 22 <<'EOL'
    echo "after build script"
    EOL
```

### Unsupported Options

- Hide command from console output
