# AWS Cloudformation

## Travis Input

```yaml
deploy:
  provider: cloudformation
  access-key-id: <encrypted access_key_id>
  secret-access-key: <encrypted secret_access_key>
  template: <template>
  stack-name: <stack_name>
  edge: true
```

## Transformed Github Action

```yaml
- uses: aws-actions/configure-aws-credentials@v1
  with:
    aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
    aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
    aws-region: "${{ inputs.region }}"

- uses: aws-actions/aws-cloudformation-github-deploy@v1.1.0
  with:
    name: "<stack_name_prefix>-<stack_name>"
    template: "<template>"
    no-execute-changeset: '1'
    role-arn: "<role_arn>"
    capabilities: "<capabilities>"
    timeout-in-minutes: "<create_timeout>"
    parameter-overrides: "<parameters>"
```

### Unsupported Options

- sts_assume_role
- wait
- output_file
- skip cleanup (deprecated)
- edge
