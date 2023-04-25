# Add Ssh Keys

## CircleCI Input

```yaml
steps:
  - add_ssh_keys:
      fingerprints:
        - "b7:35:a6:4e:9b:0d:6d:d4:78:1e:9a:97:2a:66:6b:be"
```

### Transformed Github Action

```yaml
# Ensure parameter if_key_exists is set correctly
- name: Install SSH key
  uses: shimataro/ssh-key-action@v2.5.0
  with:
    key: "${{ secrets.CIRCLE_CI_SSH_KEY }}"
    name: circle_ci_id_rsa
    known_hosts: "${{ secrets.CIRCLE_CI_KNOWN_HOSTS }}"
    if_key_exists: fail
```

### Unsupported Options

- fingerprints
