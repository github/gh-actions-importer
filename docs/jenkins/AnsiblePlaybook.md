# Ansible Playbook

## Designer Pipeline

```xml
<org.jenkinsci.plugins.ansible.AnsiblePlaybookBuilder plugin="ansible@1.1">
  <playbook>playbook/path</playbook>
  <inventory class="org.jenkinsci.plugins.ansible.InventoryContent">
    <path>inventory/path</path>
    <content>dynamic_inventory</content>
  </inventory>
  <limit>limit</limit>
  <tags>tag1,tag2</tags>
  <skippedTags>tag3,tag4</skippedTags>
  <startAtTask>task_to_start</startAtTask>
  <credentialsId/>
  <vaultCredentialsId>abc-123</vaultCredentialsId>
  <become>true</become>
  <becomeUser>become_user</becomeUser>
  <sudo>true</sudo>
  <sudoUser>sudo_user</sudoUser>
  <forks>5</forks>
  <unbufferedOutput>true</unbufferedOutput>
  <colorizedOutput>true</colorizedOutput>
  <disableHostKeyChecking>false</disableHostKeyChecking>
  <additionalParameters>additional_parameter</additionalParameters>
  <copyCredentialsInWorkspace>false</copyCredentialsInWorkspace>
  <extraVars>
    <org.jenkinsci.plugins.ansible.ExtraVar>
      <key>key1</key>
      <value>val1</value>
    </org.jenkinsci.plugins.ansible.ExtraVar>
    <org.jenkinsci.plugins.ansible.ExtraVar>
      <key>key2</key>
      <value>val2</value>
    </org.jenkinsci.plugins.ansible.ExtraVar>
  </extraVars>
</org.jenkinsci.plugins.ansible.AnsiblePlaybookBuilder>
```

### Transformed Github Action

```yaml
name: run ansible playbook
shell: bash
run: ansible-playbook playbook playbook/path -i inventory/path -s -U sudo_user -b --become-user become_user -l limit -t tag1,tag2 --skipped-tags tag3,tag4 --start-at-task task_to_start -f 5 -e "key1='val1' key2='val2'"
env:
  ANSIBLE_HOST_KEY_CHECKING: true
  ANSIBLE_FORCE_COLOR: true
  PYTHONUNBUFFERED: true
```

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
  steps {
        ansiblePlaybook(credentialsId: 'private_key', inventory: 'inventories/a/hosts', playbook: 'my_playbook.yml')
    }
  }
```

### Transformed Github Action

```yaml
- name: run ansible playbook
  shell: bash
  run: ansible-playbook playbook my_playbook.yml -i inventories/a/hosts
```

### Unsupported Options

- Ensure the `ansible-playbook` ssh credentials are present on the runner.
