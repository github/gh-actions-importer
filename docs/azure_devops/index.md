# Task Mappings

Click [here](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks) to view the full listing of built-in Azure DevOps tasks.

| Azure DevOps                                                    | GitHub                                                                                          |
| :-------------------------------------------------------------- | :---------------------------------------------------------------------------------------------- |
| [Ant](Ant.md)                                                   | run                                                                                             |
| [ArchiveFiles](ArchiveFiles.md)                                 | run                                                                                             |
| [AzureAppServiceManage](AzureAppServiceManage.md)               | Azure/login@v1, azure/cli@v1.0.7                                                                   |
| [AzureCLI](AzureCLI.md)                                         | run                                                                                             |
| [AzureFileCopy](AzureFileCopy.md)                               | run                                                                                             |
| [AzureFunction](AzureFunction.md)                               | run                                                                                             |
| [AzureFunctionApp](AzureFunctionApp.md)                         | Azure/login@v1, Azure/functions-action@v1, azure/appservice-settings@v1                         |
| [AzureKeyVault](AzureKeyVault.md)                               | Azure/login@v1, run                                                                             |
| [AzurePowershell](AzurePowershell.md)                           | Azure/login@v1, Azure/powershell@v1                                                             |
| [AzureResourceGroupDeployment](AzureResourceGroupDeployment.md) | run                                                                                             |
| [AzureRmWebAppDeployment](AzureRmWebAppDeployment.md)           | Azure/login@v1, Azure/functions-action@v1, Azure/webapps-deploy@v2,azure/appservice-settings@v1 |
| [AzureServicesSecurityStatus](AzureServicesSecurityStatus.md)   | Azure/login@v1, run                                                                             |
| [AzureWebApp](AzureWebApp.md)                                   | Azure/login@v1, Azure/webapps-deploy@v2, azure/appservice-settings@v1                           |
| [AzureWebAppContainer](AzureWebAppContainer.md)                 | Azure/login@v1, Azure/webapps-deploy@v2, azure/appservice-settings@v1                           |
| [Bash](Bash.md)                                                 | run                                                                                             |
| [BatchScript](BatchScript.md)                                   | run                                                                                             |
| [Checkout](Checkout.md)                                         | actions/checkout@v2                                                                             |
| [CmdLine](CmdLine.md)                                           | run                                                                                             |
| [colinsalmcorner.colinsalmcorner-buildtasks.replace-tokens-task.ReplaceTokens](colinsalmcorner.colinsalmcorner-buildtasks.replace-tokens-task.ReplaceTokens.md)   | cschleiden/replace-tokens@v1  |
| [ContinuousIntegration](ContinuousIntegration.md)               | on.push                                                                                         |
| [CopyFiles](CopyFiles.md)                                       | actions/github-script@v6.4.0                                                                        |
| [CopyPublishBuildArtifacts](CopyPublishBuildArtifacts.md)       |actions/actions/github-script@v6.4.0, actions/upload-artifact@v2                                     |
| [databricksDeployScripts](DatabricksDeployScripts.md)           | microsoft/install-databricks-cli@v1.0.0, microsoft/databricks-import-notebook@v1.0.0, run       |
| [DeleteFiles](DeleteFiles.md)                                   | actions/github-script@v6.4.0                                                                        |
| [DeployADFJSON](DeployAdfJson.md)                               | azure/login@v1, run                                                                             |
| [Docker](Docker.md)                                             | docker/login-action@v2                                                                          |
| [DotNetCoreCLI](DotNetCoreCLI.md)                               | run                                                                                             |
| [DownloadBuildArtifacts](DownloadBuildArtifacts.md)             | actions/download-artifact@v2                                                                    |
| [DownloadPipelineArtifact](DownloadPipelineArtifact.md)         | actions/download-artifact@v2                                                                    |
| [ExtractFiles](ExtractFiles.md)                                 | actions/github-script@v6.4.0                                                                        |
| [Gradle](Gradle.md)                                             | run                                                                                             |
| [HelmDeploy](HelmDeploy.md)                                     | run, azure/aks-set-context@v1, azure/k8s-set-context@v1                                         |
| [HelmInstaller](HelmInstaller.md)                               | azure/setup-kubectl@v1, azure/setup-helm@v3.5                                                    |
| [IISWebAppDeploymentOnMachineGroup](IisWebAppDeploymentOnMachineGroup.md.md)                               | cschleiden/webdeploy-action@v1.1.0                                                    |
| [InlineAzurePowershell](InlineAzurePowershell.md)               | azure/powershell@v1,  azure/login@v1                                                            |
| [InlinePowershell](InlinePowershell.md)                         | run                                                                                             |
| [InstallAppleCertificate](InstallAppleCertificate.md)           | run                                                                                             |
| [InstallAppleProvisioningProfile](InstallAppleProvisioningProfile.md) | run                                                                                       |
| [InvokeRestApi](InvokeRestApi.md)                               | run, azure/login@v1                                                                             |
| [Kubernetes](Kubernetes.md)                                     | run, azure/setup-kubectl@v1, azure/aks-set-context@v1, azure/k8s-set-context@v1, azure/k8s-create-secret@v1 |
| [Maven](Maven.md)                                               | actions/setup-java@v3.10.0, run                                                                      |
| [ManualIntervention](ManualIntervention.md)                     |                                                                                                 |
| [MsBuild](MsBuild.md)                                           | microsoft/setup-msbuild@v1.3.1, run                                                             |
| [Npm](Npm.md)                                                   | run                                                                                             |
| [NodeTool](NodeTool.md)                                         | actions/setup-node@v1                                                                           |
| [NuGetAuthenticate](NuGetAuthenticate.md)                       | actions/setup-dotnet@v3                                                                         |
| [NuGetCommand](NuGetCommand.md)                                 | run                                                                                             |
| [NuGetInstaller](NuGetCommand.md#NuGetInstaller)                | run                                                                                             |
| [NuGetToolInstaller](NuGetToolInstaller.md)                     | nuget/setup-nuget@v1.1.1                                                                        |
| [PowerShell](PowerShell.md)                                     | run                                                                                             |
| [PowerShellOnTargetMachines](PowerShellOnTargetMachines.md)     | azure/login@v1, azure/powershell@v1                                                             |
| [PublishBuildArtifacts](PublishBuildArtifacts.md)               | actions/upload-artifact@v2                                                                      |
| [PublishCodeCoverageResults](PublishCodeCoverageResults.md)     | actions/upload-artifact@v2, danielpalme/ReportGenerator-GitHub-Action@4.8.12                    |
| [PublishPipelineArtifact](PublishPipelineArtifact.md)           | actions/upload-artifact@v2                                                                      |
| [PublishSymbols](PublishSymbols.md)                             | microsoft/action-publish-symbols@v1                                                             |
| [PublishTestResults](PublishTestResults.md)                     | EnricoMi/publish-unit-test-result-action@v2.4.1 or dorny/test-reporter@v1.6.0                     |
| [PullRequest](PullRequest.md)                                   | on.pull_request                                                                                 |
| [PythonScript](PythonScript.md)                                 | run                                                                                             |
| [ReplaceTokens](ReplaceTokens.md)                               | cschleiden/replace-tokens@v1                                                                    |
| [Shell++](ShellPlusPlus.md)                                     | run                                                                                             |
| [SSISBuild](SSISBuild.md)                                       | run                                                                                             |
| [SqlAzureDacpacDeployment](SqlAzureDacpacDeployment.md)         | run, Azure/login@v1, sql-action@v1                                                              |
| [SqlDacpacDeploy](SqlDacpacDeploy.md)                           | run, Azure/login@v1, sql-action@v1                                                              |
| [SqlDacpacDeploymentOnMachineGroup](SqlDacpacDeploymentOnMachineGroup.md) | run                                                                                   |
| [TerraformInstaller](TerraformInstaller.md)                     | hashicorp/setup-terraform@v2                                                                    |
| [Terraform](Terraform.md)                                       | run                                                                                             |
| [Tokenization](Tokenization.md)                                 | cschleiden/replace-tokens@v1                                                                    |
| [Tokenizer](Tokenizer.md)                                       | run                                                                                             |
| [Toggle-ADF-Trigger](ToggleAdfTrigger.md)                       | azure/login@v1, run                                                                             |
| [UseDotNet](UseDotNet.md)                                       | actions/setup-dotnet@v1                                                                         |
| [UseRubyVersion](UseRubyVersion.md)                             | ruby/setup-ruby@v1.138.0                                                                              |
| [UsePythonVersion](UsePythonVersion.md)                         | actions/setup-python@v1                                                                         |
| [VsBuild](MsBuild.md)                                           | microsoft/setup-msbuild@v1.3.1, run                                                             |
| [VSTest](VsTest.md)                                             | microsoft/vstest-action@v1.0.0         |
| [WindowsMachineFileCopy](WindowsMachineFileCopy.md)             | run                                                                                             |
| [XamarinAndroid](XamarinAndroid.md)                             | actions/setup-java@v3.10.0, microsoft/setup-msbuild@v1.3.1, run                                      |
| [Xamarin iOS](XamarinIos.md)                                    | run                                                                                             |
| [Xcode](Xcode.md)                                               | run, maxim-lobanov/setup-xcode@v1                                                               |
| [Yarn](Yarn.md)                                                 | run                                                                                             |
| [YarnInstaller](YarnInstaller.md)                               | run                                                                                             |

## Unsupported

The following tasks do not have any equivalent in GitHub Actions:

- BuildQualityChecks
- Package
- ArtifactSource
- PublishSecurityAnalysisLogs

Any task not listed above will not be mapped to an action and will be left as a comment in the converted workflow.
