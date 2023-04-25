# Terraform Build Wrapper

## Designer Pipeline

### Jenkins Input

```xml
<org.jenkinsci.plugins.terraform.TerraformBuildWrapper plugin="terraform@1.0.10">
  <variables>var1 = envvar1
var2 = envvar2</variables>
  <doDestroy>false</doDestroy>
  <doGetUpdate>false</doGetUpdate>
  <doNotApply>false</doNotApply>
  <config>
    <value>inline</value>
    <inlineConfig>provider &quot;azurerm&quot; {
subscription_id = &quot;ABC&quot;
client_id       = &quot;123&quot;
client_secret   = &quot;secret0123&quot;
tenant_id       = &quot;tenant123&quot;
}
 
variable &quot;location&quot; { default = &quot;US West&quot; }
 
resource &quot;azurerm_resource_group&quot; &quot;test&quot; {
name     = &quot;HelloWorld&quot;
location = &quot;${var.location}&quot;
}</inlineConfig>
    <mode>INLINE</mode>
  </config>
  <terraformInstallation>Terraform01</terraformInstallation>
</org.jenkinsci.plugins.terraform.TerraformBuildWrapper>
```

### Transformed Github Action

```yaml
- uses: hashicorp/setup-terraform@v1
  with:
    terraform_version: "${{env.TERRAFORM_VERSION}}"
- name: emit terraform inline script
  working_directory: "./"
  shell: bash
  run: |-
    cat >$RUNNER_TEMP/terraform.tf <<'EOL'
    provider "azurerm" {
    subscription_id = "ABC"
    client_id       = "123"
    client_secret   = "secret0123"
    tenant_id       = "tenant123"
    }
    variable "location" { default = "US West" }
    resource "azurerm_resource_group" "test" {
    name     = "HelloWorld"
    location = "${var.location}"
    }
    EOL
- name: emit terraform variables
  working_directory: "./"
  shell: bash
  run: |-
    cat >$RUNNER_TEMP/variables.tfvars <<'EOL'
    var1 = envvar1
    var2 = envvar2
    EOL
- name: execute terraform commands
  working_directory: "./"
  run: |-
    terraform init
    terraform apply -var-file=variables.tfvars
```

### Unsupported Options

- None

### Notes

GitHub action performs as a build wrapper for the subsequent workflow steps. The transformation requires setting up the environmental variable to specify the Terraform version used by the workflow.
