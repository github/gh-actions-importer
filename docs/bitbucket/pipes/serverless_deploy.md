# Serverless Deploy

[BitBucket Serverless Deploy Documentation](https://bitbucket.org/atlassian/serverless-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/serverless-deploy:1.3.0
variables:
  CONFIG: my-custom-config.yml
  PRE_EXECUTION_SCRIPT: run-first.sh
  AWS_ACCESS_KEY_ID: my-access-key
  AWS_SECRET_ACCESS_KEY: $AWS_SECRET_SERVERLESS_DEPLOY_KEY
```

## Transformed GitHub Action
```yaml
- name: Execute pre-execution script
  shell: sh
  run: "./run-first.sh"
- uses: serverless/github-action@v3.2.0
  env:
    AWS_ACCESS_KEY_ID: my-access-key
    AWS_SECRET_ACCESS_KEY: "${{ env.AWS_SECRET_SERVERLESS_DEPLOY_KEY }}"
  with:
    args: serverless deploy -c my-custom-config.yml
```

## Unsupported Options
* DEBUG
