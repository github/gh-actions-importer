# AWS Code Deploy

## Bamboo input

```yaml
Deploy:
...
  - any-task:
      plugin-key: com.atlassian.bamboo.plugins.atlassian-bamboo-plugin-aws-codedeploy:task.aws.codeDeploy
      configuration:
        deploymentTimeout: '15'
        deploymentGroup: DgpECS-begona_cluster-alt_service
        credentialsId: '10092545'
        region: US_WEST_1
        applicationName: AppECS-begona_cluster-alt_service
        s3Bucket: FA
```

## Transformed Github Action

```yaml
    - uses: aws-actions/configure-aws-credentials@v2
      with:
        aws-region: us-west-1
        aws-access-key-id: "${{ secrets.AWS_ACCESS_KEY_ID }}"
        aws-secret-access-key: "${{ secrets.AWS_SECRET_ACCESS_KEY }}"
   - run: |-
        commit_hash=$(git rev-parse --short ${{ env.GITHUB_SHA }})
        aws deploy create-deployment --application-name AppECS-begona_cluster-alt_service --deployment-group-name DgpECS-begona_cluster-alt_service --s3-location bucket=FA,key=${{ secrets.AWS_S3_BUCKET_KEY }},bundleType={{ env.BUNDLE_TYPE }} --github-location repository=$GITHUB_REPOSITORY,commitId=$commit_hash --ignore-application-stop-failures
```

## Unsupported Options
- credentialsId
