# Ssh

## Bamboo input

```yaml
- ssh:
    host:
    - 20.230.215.196
    - github.com
    command: "ls -alh\r\necho \"hi\""
    authentication:
      username: bamboo
      ssh-key: BAMSCRT@0@0@rdrTIEVbeDoC/QU4C/v9UuKFI3aL5xBu5/rS1G6hTmDX
      ssh-key-passphrase: BAMSCRT@0@0@ZghWfw5lfrhdT8XIYWqV3A==
    description: SSH Fun!
```

## Transformed Github Action

```yaml
# Ensure parameter if_key_exists is set correctly
name: Install SSH key
uses: shimataro/ssh-key-action@v2.5.0
with:
  key: "${{ secrets.BAMBOO_SSH_KEY }}"
  name: id_rsa
  known_hosts: "${{ secrets.BAMBOO_KNOWN_HOSTS }}"
  if_key_exists: fail
name: Install sshpass
run: sudo apt-get install -y sshpass
name: SSH 20.230.215.196
run: |-
  sshpass -p ${{ secrets.BAMBOO_SSH_PASSWORD }} ssh -o StrictHostKeyChecking=no -T bamboo@20.230.215.196 -i id_rsa << EOF
    ls -alh
    echo "hi"
  EOF
name: SSH github.com
run: |-
  sshpass -p ${{ secrets.BAMBOO_SSH_PASSWORD }} ssh -o StrictHostKeyChecking=no -T bamboo@github.com -i id_rsa << EOF
    ls -alh
    echo "hi"
  EOF
```

## Unsupported Options

- description
