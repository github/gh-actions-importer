# Credentials

## Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.credentialsbinding.impl.SecretBuildWrapper plugin="credentials-binding@1.23">
    <bindings>
    <org.jenkinsci.plugins.credentialsbinding.impl.CertificateMultiBinding>
        <credentialsId></credentialsId>
        <keystoreVariable></keystoreVariable>
        <passwordVariable></passwordVariable>
        <aliasVariable></aliasVariable>
    </org.jenkinsci.plugins.credentialsbinding.impl.CertificateMultiBinding>
    <org.jenkinsci.plugins.credentialsbinding.impl.SSHUserPrivateKeyBinding>
        <credentialsId></credentialsId>
        <keyFileVariable></keyFileVariable>
        <usernameVariable></usernameVariable>
        <passphraseVariable></passphraseVariable>
    </org.jenkinsci.plugins.credentialsbinding.impl.SSHUserPrivateKeyBinding>
    <org.jenkinsci.plugins.credentialsbinding.impl.ZipFileBinding>
        <credentialsId></credentialsId>
        <variable></variable>
    </org.jenkinsci.plugins.credentialsbinding.impl.ZipFileBinding>
    <org.jenkinsci.plugins.credentialsbinding.impl.FileBinding>
        <credentialsId></credentialsId>
        <variable></variable>
    </org.jenkinsci.plugins.credentialsbinding.impl.FileBinding>
    <org.jenkinsci.plugins.credentialsbinding.impl.UsernamePasswordBinding>
        <credentialsId></credentialsId>
        <variable></variable>
    </org.jenkinsci.plugins.credentialsbinding.impl.UsernamePasswordBinding>
    <org.jenkinsci.plugins.credentialsbinding.impl.UsernamePasswordMultiBinding>
        <credentialsId></credentialsId>
        <usernameVariable></usernameVariable>
        <passwordVariable></passwordVariable>
    </org.jenkinsci.plugins.credentialsbinding.impl.UsernamePasswordMultiBinding>
    <org.jenkinsci.plugins.credentialsbinding.impl.StringBinding>
        <credentialsId>${test}</credentialsId>
        <variable>test</variable>
    </org.jenkinsci.plugins.credentialsbinding.impl.StringBinding>
    </bindings>
</org.jenkinsci.plugins.credentialsbinding.impl.SecretBuildWrapper>
```

### Transformed Github Action

```yaml
env:
  test: ${test}
```

### Unsupported Options

- Certificate (org.jenkinsci.plugins.credentialsbinding.impl.CertificateMultiBinding)
- Secret ZIP File (org.jenkinsci.plugins.credentialsbinding.impl.ZipFileBinding)
- Secret File (org.jenkinsci.plugins.credentialsbinding.impl.FileBinding)
- Kubeconfig File (com.microsoft.jenkins.kubernetes.credentials.KubeconfigFileCredentialsBinding)

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
withCredentials([
    usernamePassword(
        credentialsId: 'ssshhh',
        passwordVariable: 'PASSWORD',
        usernameVariable: 'USER_NAME')]) {
    echo "Hi ${USER_NAME} your password is ${PASSWORD}"
}
```

### Transformed Github Action

```yaml
- name: echo message
  run: echo "Hi ${{ env.USER_NAME }}"
  env:
    PASSWORD: "${{ secrets.ssshhh_PASSWORD }}"
    USER_NAME: "${{ secrets.ssshhh_USER_NAME }}"
```

### Unsupported Bindings
- certificate
- file
- zip
- KeychainPasswordAndPathBinding
- kubeconfigFile
- ConjurSecretApplianceCredentials
- conjurSecretCredential
- conjurSecretUsername
- conjurSecretUsernameSSHKey
