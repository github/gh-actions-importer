# Google Gke Kubectl Run

[BitBucket Google Gke Kubectl Run Documentation](https://bitbucket.org/atlassian/google-gke-kubectl-run)

## Bitbucket Input

```yaml
- pipe: atlassian/google-gke-kubectl-run:3.0.0
  variables:
    KEY_FILE: my-key-file.json
    PROJECT: 'pipes-kube-web-app'
    COMPUTE_ZONE: 'us-east1'
    CLUSTER_NAME: 'pipes-kube-cluster'
    KUBECTL_COMMAND: 'apply'
    RESOURCE_PATH: 'nginx.yml'
    KUBECTL_ARGS:
      - '--dry-run'
```

## Transformed GitHub Action
```yaml
- uses: google-github-actions/auth@v1.1.1
  with:
    credentials_json: my-key-file.json
- uses: google-github-actions/setup-gcloud@v1.1.1
  with:
    project_id: pipes-kube-web-app
- run: |
    gcloud config set compute/zone us-east1 --quiet
    cmd=(gcloud container clusters get-credentials pipes-kube-cluster)
    $cmd
  env:
    KUBECTL_COMMAND: apply
    KUBECTL_ARGS: "--dry-run"
    KUBECTL_APPLY_ARGS: "-f"
    RESOURCE_PATH: nginx.yml
    WITH_DEFAULT_LABELS: true
```

## Unsupported Options
* DEBUG
