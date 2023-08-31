# Secrets

## GitLab Input

```yaml
secrets:
  DATABASE_PASSWORD:
    vault:
      engine:
        name: kv-v2
        path: ops
      path: production/db
      field: password
  ADMIN_PASSWORD:
    vault: production/db/admin_password
```

### Transformed Github Action

```yaml
- uses: hashicorp/vault-action@v2.5.0
  env:
    VAULT_URL: UPDATE_THIS_VALUE
  with:
    url: "${{ env.VAULT_URL }}"
    token: "${{ secrets.VaultToken }}"
    secrets: |-
      ops/data/production/db password | DATABASE_PASSWORD ;
      kv-v2/data/production/db admin_password | ADMIN_PASSWORD
```

### Unsupported Options

- file: true
