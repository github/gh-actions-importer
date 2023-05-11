# Migrating Bamboo Build Plans and Deployment Projects with Github Actions Importer

## How to run GitHub Actions Importer

GitHub Actions Importer can be run locally as a CLI, allowing a user to perform a audit, migration, and dry run.
A best attempt at a full-fidelity transformation of a Bamboo plan or deployment project will be attempted, but each converted workflow should be reviewed and edited as necessary.

### Perform an audit of Bamboo Instance
You can use the audit command to get a high-level view of all build plans and deployment projects in a Bamboo instance.

The audit command performs the following steps:
- Fetches all of the build plans.
- Fetches all of the deployment projects.
- Converts each build plan and deployment project to its equivalent GitHub Actions workflow.
- Generates a report that summarizes how complete and complex of a migration is possible with GitHub Actions Importer.

To perform an audit run the following command in your terminal:

```bash
gh actions-importer audit bamboo --output-dir tmp/audit
```

Optionally, a `--project` option can be provided to the audit command to limit the results to only build plans and deployment projects associated with a build project.
In the below example command `:project_key` should be replaced with the project key of the build project that should be audited.

```bash
gh actions-importer audit bamboo --project :project_key --output-dir tmp/audit
```
### Perform a dry-run migration
You can use the dry-run command to convert an Bamboo build plan or deployment project to an equivalent GitHub Actions workflow. A dry run creates the output files in a specified directory, but does not open a pull request to migrate the pipeline.

#### Running the dry-run command for a build plan
To perform a dry run of migrating your Bamboo build plan to GitHub Actions, run the following command in your terminal, replacing `:plan_slug` with the plan's project and plan key in the format `<projectKey>-<planKey>` (for example: `PAN-SCRIP`).
The project and plan key can be found in this form at the end of the URL when you are viewing the plan in the Bamboo UI.

```bash
gh actions-importer dry-run bamboo build --plan-slug :plan_slug --output-dir tmp/dry-run
```

You can view the logs of the dry run and the converted workflow files in the specified output directory.

#### Running the dry-run command for a deployment project
To perform a dry run of migrating your Bamboo deployment project to GitHub Actions, run the following command in your terminal, replacing `:deployment_project_id` with the ID of the deployment project you are converting.
The easiest location to get this ID is from the end of the URL when you are viewing the project in the Bamboo UI.

```bash
gh actions-importer dry-run bamboo deployment --deployment-project-id :deployment_project_id --output-dir tmp/dry-run
```
You can view the logs of the dry run and the converted workflow files in the specified output directory.
> **Note**
> Each deployment environment will be transformed into its own workflow.

### Perform a production migration
You can use the migrate command to convert an Bamboo build plan or deployment project and open a pull request with the equivalent GitHub Actions workflow.

#### Running the migrate command for a build plan
To migrate an Bamboo build plan to GitHub Actions, run the following command in your terminal, replacing `:target_url` with the URL for your GitHub repository, and `:plan_slug` with the plan's project and plan key in the format `<projectKey>-<planKey>`.

```bash
gh actions-importer migrate bamboo build --plan-slug :plan_slug --target-url :target_url --output-dir tmp/migrate
```
The command's output includes the URL of the pull request that adds the converted workflow to your repository. An example of a successful output is similar to the following:

```bash
$ gh actions-importer migrate bamboo build --plan-slug :plan_slug --target-url https://github.com/octo-org/octo-repo --output-dir tmp/migrate
[2023-04-20 22:08:20] Logs: 'tmp/migrate/log/actions-importer-20230420-014033.log'
[2023-04-20 22:08:20] Pull request: 'https://github.com/octo-org/octo-repo/pull/1'
```

#### Running the migrate command for a deployment plan
To migrate an Bamboo deployment plan to GitHub Actions, run the following command in your terminal, replacing the `:target_url` with the URL for your GitHub repository, and `:deployment_project_id` with the ID of the deployment project you are converting.

```bash
gh actions-importer migrate bamboo deployment --deployment-project-id :deployment_project_id --target-url :target_url --output-dir tmp/migrate
```
The command's output includes the URL of the pull request that adds the converted workflow to your repository. An example of a successful output is similar to the following:

```bash
$ gh actions-importer migrate bamboo deployment --deployment-project-id 123 --target-url https://github.com/octo-org/octo-repo --output-dir tmp/migrate
[2023-04-20 22:08:20] Logs: 'tmp/migrate/log/actions-importer-20230420-014033.log'
[2023-04-20 22:08:20] Pull request: 'https://github.com/octo-org/octo-repo/pull/1'
```

### Using environment variables

We recommend maintaining the inputs to GitHub Actions Importer with environment variables that can be set by following the instructions [here](../README.md#authenticating-at-the-command-line).

The following are environment variables used by GitHub Actions Importer to connect to your Bamboo instance.

- `GITHUB_ACCESS_TOKEN`: The personal access token used to create pull requests with a transformed workflow (requires `repo` and `workflow` scopes).
- `GITHUB_INSTANCE_URL`: The url to the target GitHub instance. (e.g. `https://github.com`)
- `BAMBOO_INSTANCE_URL`: The url to the Bamboo Instance.
- `BAMBOO_ACCESS_TOKEN`: The private token used to authenticate with your Bamboo instance. This token requires the following permissions for the resources that will be transformed.

    Resource Type | View | View Configuration | Edit
    |:---: | :---: | :---: | :---:
    | Build Plan | X | X | X
    | Deployment Project | X | X | -
    | Deployment Environment | X |-| -

    > **Note**
    > The edit permission is required for a plan to get the associated variables.

These environment variables can be specified in a `.env.local` file that will be loaded by GitHub Actions Importer at run time. The distribution archive contains a `.env.local.template` file that can be used to create these files.

### Optional arguments

When using the `--source-file-path` or `--config-file-path` CLI options, ensure that the YAML that is saved on disk is the YAML that Bamboo provides from the `View plan as YAML` option in the UI.

#### Source file path

The `--source-file-path` CLI option can be set to feed a single source file into GitHub Actions Importer for a dry run or migration. GitHub Actions Importer will use the source file specified instead of fetching the pipeline contents from source control.

```bash
gh actions-importer dry-run bamboo build --plan-slug IN-COM -o tmp/bamboo --source-file-path path/to/my/bamboo/file.yml
```

#### Config file path

The `--config-file-path` CLI option specifies the path to a config file that can do the following:
- [Feed a list of source files into GitHub Actions Importer for an audit](#pulling-multiple-pipelines)
- [Feed a single source file into GitHub Actions Importer for a migration or dry run](#pulling-a-single-pipeline)

#### <b>Pulling multiple pipelines</b>

Using the `--config-file-path` CLI option during an audit will alert GitHub Actions Importer to ingest the source files instead of fetching pipeline source code from source control.

```bash
gh actions-importer audit bamboo -o tmp/bamboo --config-file-path "path/to/my/bamboo/config.yml"
```

To audit a Bamboo instance using a config file, the config file should be in the following format:

```yaml
source_files:
  - repository_slug: IN/COM
    path: path/to/one/source/file.yml
  - repository_slug: IN/JOB
    path: path/to/another/source/file.yml
```

**Note**: Each repository slug value must be unique.

#### <b>Pulling a single pipeline</b>

The `--config-file-path` CLI option can also be used to feed a single source file into GitHub Actions Importer for a dry run or migration. GitHub Actions Importer will use the config file specified instead of fetching from Bamboo.

The repository slug will be built using the `--plan-slug` option. The source file path will then be matched and pulled from the specified source file.


```bash
gh actions-importer dry-run bamboo build --plan-slug IN-COM -o tmp/bamboo --config-file-path "path/to/my/bamboo/config.yml"
```
## Supported Syntax

| Bamboo                              | GitHub Actions                                  |  Status                |
| :---------------------------------- | :-----------------------------------------------| ---------------------: |
| `triggers`                          | `on`                                            |  Supported             |         |
| `stages`                            | `jobs.<job_id>.needs`                           |  Supported             |
| `stages.<stage_id>.jobs`            | `jobs`                                          |  Supported             |
| `stages.<stage_id>.manual`          | `jobs.<job_id>.environment`                     |  Supported             |
| `stages.<stage_id>.final`           | `jobs.<job_id>.if`                              |  Supported             |
| `stages.<stage_id>.jobs.<job_id>`   | `jobs.<job_id>`                                 |  Supported             |
| `<job_id>.requirements`             | `jobs.<job_id>.runs-on`                         |  Supported             |
| `<job_id>.docker`                   | `jobs.<job_id>.container`                       |  Supported             |
| `<job_id>.variables`                | `jobs.<job_id>.env`                             |  Supported             |
| `<job_id>.artifact-subscriptions`   | `jobs.<job_id>.steps.actions/download-artifact` |  Supported             |
| `<job_id>.artifacts`                | `jobs.<job_id>.steps.actions/upload-artifact`   |  Supported             |
| `<job_id>.tasks`                    | `jobs.<job_id>.steps`                           |  Supported             |
| `<job_id>.final-tasks`              | `jobs.<job_id>.steps.if`                        |  Supported             |
| `environments`                      | `jobs`                                          |  Supported             |
| `environments.<environment_id>`     | `jobs.<job_id>`                                 |  Supported             |
| `dependencies`                      | `jobs.<job_id>.steps.<gh cli step>`             |  Partially Supported   |
| `branches`                          |                                                 |  Unsupported           |
| `notifications`                     |                                                 |  Unsupported           |
| `repositories`                      |                                                 |  Unsupported           |
| `release-naming`                    |                                                 |  Unsupported           |
| `plan-permissions`                  |                                                 |  Unsupported           |
| `environment-permissions`           |                                                 |  Unsupported           |
| `deployment.deployment-permissions` |                                                 |  Unsupported           |

### Environment variables

#### Bamboo Defaults
GitHub Actions Importer will convert default Bamboo environment variables to the [closest equivalent](https://docs.github.com/en/actions/learn-github-actions/contexts#github-context) in GitHub Actions using the mapping below:

| Bamboo                                           | GitHub Actions                                      |
| :----------------------------------------------- | :-------------------------------------------------- |
| `bamboo.buildKey`                                | `${{ github.workflow }}-${{ github.job }}`
| `bamboo.planKey`                                 | `${{ github.workflow }}`
| `bamboo.shortPlanKey`                            | `${{ github.workflow }}`
| `bamboo.shortJobKey`                             | `${{ github.job }}`
| `bamboo.buildResultKey`                          | `${{ github.workflow }}-${{ github.job }}-${{ github.run_id }}`
| `bamboo.buildResultsUrl`                         | `${{ github.server_url }}/${{ github.repository }}/actions/runs/${{ github.run_id }}`
| `bamboo.resultsUrl`                              | `${{ github.server_url }}/${{ github.repository }}/actions/runs/${{ github.run_id }}`
| `bamboo.buildNumber`                             | `${{ github.run_id }}`
| `bamboo.buildPlanName`                           | `${{ github.repository }}-${{ github.workflow }}-${{ github.job }`
| `bamboo.planName`                                | `${{ github.repository }}-${{ github.workflow }}`
| `bamboo.shortPlanName`                           | `${{ github.workflow }}`
| `bamboo.shortJobName`                            | `${{ github.job }}`
| `bamboo.agentId`                                 | `${{ github.runner_name }}`
| `bamboo.agentWorkingDirectory`                   | `${{ github.workspace }}`
| `bamboo.build.working.directory`                 | `${{ github.workspace }}`
| `bamboo.ManualBuildTriggerReason.userName`       | `${{ github.actor }}`
| `bamboo.planRepository.<position>.branchName`    | `${{ github.ref }}`
| `bamboo.planRepository.<position>.name`          | `${{ github.repository }}`
| `bamboo.planRepository.<position>.revision`      | `${{ github.sha }}`
| `bamboo.repository.pr.key`                       | `${{ github.event.pull_request.number }}`
| `bamboo.repository.pr.sourceBranch`              | `${{ github.event.pull_request.head.ref }}`
| `bamboo.repository.pr.targetBranch`              | `${{ github.event.pull_request.base.ref }}`
| `bamboo.planRepository.branchDisplayName`        | `${{ github.ref }}`
| `bamboo.planRepository.<position>.username`      | `${{ github.actor}}`
| `bamboo.planRepository.<position>.repositoryUrl` | `${{ github.server }}/${{ github.repository }}`
| `bamboo.planRepository.<position>.branch`        | `${{ github.ref }}`
| `bamboo.deploy.project`                          | `${{ github.repository }}`
| `bamboo.repository.branch.name`                  | `${{ github.ref }}`
| `bamboo.repository.git.branch`                   | `${{ github.ref }}`
| `bamboo.repository.git.repositoryUrl`            | `${{ github.server }}/${{ github.repository }}`

**Note:** Unknown variables will be transformed to `${{ env.<variableName> }}` and will need to be replace or added under `env` for proper operation, for example `${bamboo.jira.baseUrl}` will become `${{ env.jira_baseUrl }}`.

### System Variables

System variables (`${system.<variable.name>}`) used in tasks will be transformed to the equivalent bash shell variable (`$variable_name`) and are assumed to be available. This should be verified to ensure proper operation of the workflow.

## Forecast
This feature is currently not available for Bamboo.

## Limitations

### General

- GitHub Actions Importer relies on the YAML spec generated by the Bamboo Server to perform the migration. There are cases where Bamboo does not support exporting something to YAML, in these cases the missing information will not be part of the migrated workflow(s).
The majority of known cases are for third-party plugins, which will show in the migrated workflow as unsupported if the plugin key is provided but the configuration is missing in the YAML.
- Trigger conditions are not supported because there is no suitable equivalent in GitHub Actions. When a trigger is encountered with a condition the condition will be surfaced as a comment and the trigger will be transformed without it.
- Bamboo Plans can have customized settings for how the Bamboo instance stores artifacts. This behavior is not transformed and instead artifacts will be stored and retrieved using the [upload-artifact](https://github.com/actions/upload-artifac) and [download-artifact](https://github.com/actions/download-artifact) actions.
- Disabled plans will be transformed as is and **will need to be disabled** in the GitHub UI by following the instructions [here](https://docs.github.com/en/actions/managing-workflow-runs/disabling-and-enabling-a-workflow). A manual task will be added to the PR for this during a migration.
- Disabled jobs will be transformed with a `if: false` condition preventing it from running and will need to be removed to re-enable.
- Disabled tasks will not be transformed because they are not included in the exported plan when using the Bamboo API.

### Artifacts

All artifacts are transformed into an `actions/upload-artifact` regardless of whether they are `shared` or not. This means they can be downloaded from any job in the workflow.

### Other Job Settings

- Bamboo provides options to clean up the build workspace after the build is complete. This will not be transformed because it is assumed a GitHub-hosted or ephemeral self-hosted runner will be used and this will be automatically handled.
- The hanging build detection options will not be transformed because there is no equivalent in GitHub Actions. The closest option is `timeout-minutes` on a job, which can be used to set the maximum number of minutes to let a job run.
- Pattern match labelling will not be transformed because there is no equivalent in GitHub Actions.

### Permissions  
Permissions will not be transformed because there is no suitable equivalent in GitHub Actions.

### Manual tasks

Certain constructs need to be migrated manually. These include:

- Masked variables
- Artifact expiry settings.
