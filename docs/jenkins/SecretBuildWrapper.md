# SecretBuildWrapper

## Designer Pipeline

### Jenkins Input

```xml
   <org.jenkinsci.plugins.credentialsbinding.impl.SecretBuildWrapper>
      <bindings>
         <org.jenkinsci.plugins.credentialsbinding.impl.CertificateMultiBinding>
            <credentialsId />
            <keystoreVariable>TEST_KEYSTORE</keystoreVariable>
            <passwordVariable>TEST_PASS</passwordVariable>
            <aliasVariable>TEST_ALIAS</aliasVariable>
         </org.jenkinsci.plugins.credentialsbinding.impl.CertificateMultiBinding>
         <org.jenkinsci.plugins.docker.commons.credentials.DockerServerCredentialsBinding>
            <credentialsId />
            <variable>DOCKER_CERT_PATH</variable>
         </org.jenkinsci.plugins.docker.commons.credentials.DockerServerCredentialsBinding>
         <com.microsoft.jenkins.kubernetes.credentials.KubeconfigCredentialsBinding>
            <credentialsId />
            <variable>KUBECONFIG_CONTENT</variable>
         </com.microsoft.jenkins.kubernetes.credentials.KubeconfigCredentialsBinding>
         <com.microsoft.jenkins.kubernetes.credentials.KubeconfigFileCredentialsBinding>
            <credentialsId />
            <variable>KUBECONFIG</variable>
         </com.microsoft.jenkins.kubernetes.credentials.KubeconfigFileCredentialsBinding>
         <com.microsoft.azure.util.AzureCredentialsBinding>
            <credentialsId />
            <subscriptionIdVariable>AZURE_SUBSCRIPTION_ID</subscriptionIdVariable>
            <clientIdVariable>AZURE_CLIENT_ID</clientIdVariable>
            <clientSecretVariable>AZURE_CLIENT_SECRET</clientSecretVariable>
            <tenantIdVariable>AZURE_TENANT_ID</tenantIdVariable>
         </com.microsoft.azure.util.AzureCredentialsBinding>
         <com.microsoftopentechnologies.windowsazurestorage.helper.AzureCredentialsBinding>
            <credentialsId />
            <storageAccountNameVariable>AZURE_STORAGE_ACCOUNT_NAME</storageAccountNameVariable>
            <storageAccountKeyVariable>AZURE_STORAGE_ACCOUNT_KEY</storageAccountKeyVariable>
            <blobEndpointUrlVariable>AZURE_BLOB_ENDPOINT_URL</blobEndpointUrlVariable>
         </com.microsoftopentechnologies.windowsazurestorage.helper.AzureCredentialsBinding>
         <org.jenkinsci.plugins.credentialsbinding.impl.SSHUserPrivateKeyBinding>
            <credentialsId />
            <keyFileVariable>TEST_KEY</keyFileVariable>
            <usernameVariable>TEST_USERNAME</usernameVariable>
            <passphraseVariable>TEST_PARAPHRASE</passphraseVariable>
         </org.jenkinsci.plugins.credentialsbinding.impl.SSHUserPrivateKeyBinding>
         <org.jenkinsci.plugins.credentialsbinding.impl.ZipFileBinding>
            <credentialsId>ec6e2a8e-709a-4c29-b1fe-ec008ea27da0</credentialsId>
            <variable>TEST_ZIP_FILE</variable>
         </org.jenkinsci.plugins.credentialsbinding.impl.ZipFileBinding>
         <org.jenkinsci.plugins.credentialsbinding.impl.FileBinding>
            <credentialsId>ec6e2a8e-709a-4c29-b1fe-ec008ea27da0</credentialsId>
            <variable>TEST_FILE</variable>
         </org.jenkinsci.plugins.credentialsbinding.impl.FileBinding>
         <org.jenkinsci.plugins.credentialsbinding.impl.StringBinding>
            <credentialsId />
            <variable>TEST_TEXT</variable>
         </org.jenkinsci.plugins.credentialsbinding.impl.StringBinding>
         <org.jenkinsci.plugins.credentialsbinding.impl.UsernamePasswordBinding>
            <credentialsId>6aae0c55-addb-47f0-a8ee-2b78985db2aa</credentialsId>
            <variable>TEST_USERNAME_PASSWORD</variable>
         </org.jenkinsci.plugins.credentialsbinding.impl.UsernamePasswordBinding>
      </bindings>
   </org.jenkinsci.plugins.credentialsbinding.impl.SecretBuildWrapper>
</buildWrappers>
```

### Transformed Github Action

```yaml
env:
  TEST_TEXT: "${TEST_PARAM}"
```

### Unsupported Options

- Certificate
- SSH User private Key
- Zecret ZIP File
- Secret File
- Username and password (conjoined)
- Username and password (separated)

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
environment {
  color = "blue"
}
```

### Transformed Github Action

```yaml
env:
  color: blue
```

### Unsupported Options

- Groovy expressions
