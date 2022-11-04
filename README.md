# GitHub Actions Importer

[![.github/workflows/ci.yml](https://github.com/github/gh-actions-importer/actions/workflows/ci.yml/badge.svg)](https://github.com/github/gh-actions-importer/actions/workflows/ci.yml)

[GitHub Actions Importer](https://docs.github.com/en/actions/migrating-to-github-actions/automating-migration-with-github-actions-importer) helps plan, forecast, and automate the migration of Azure DevOps, CircleCI, GitLab, Jenkins, and Travis CI pipelines to GitHub Actions. This repository provides functionality that extends the [GitHub CLI](https://cli.github.com/) to migrate pipelines using the GitHub Actions Importer.

> **Note**: Sign up [here](https://github.com/features/actions-importer/signup) to request access to the public preview for the GitHub Actions Importer. Once you are granted access you'll be able to use the `gh-actions-importer` CLI extension

You can request support [here](https://support.github.com/contact?tags=actions_importer_beta) on a best-effort basis during the public preview period.

## Supported platforms

GitHub Actions Importer currently supports migrating pipelines to GitHub Actions from the following platforms:

- Azure DevOps
- CircleCI
- GitLab
- Jenkins
- Travis CI

You can find detailed information about how the GitHub Actions Importer works for each of the supported platforms in the documentation that is available once you are granted access.

## Getting started with GitHub Actions Importer

GitHub Actions Importer is distributed as a Docker container and this extension to the official [GitHub CLI](https://cli.github.com) to interact with the Docker container.

### Prerequisites

The following requirements must be met to be able to use the GitHub Actions Importer:

- The Docker CLI must be [installed](https://docs.docker.com/get-docker/) and running
- The official [GitHub CLI](https://cli.github.com) must be installed
- You must have credentials to [authenticate](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-container-registry#authenticating-to-the-container-registry) with the GitHub Container Registry after you are granted access.

### Installation

Next, the GitHub Actions Importer CLI extension can be installed via this command:

```bash
$ gh extension install github/gh-actions-importer
```

To verify the extension is installed, run this command:

```bash
$ gh actions-importer -h
Options:
  -?, -h, --help  Show help and usage information

Commands:
  update     Update to the latest version of the GitHub Actions Importer.
  version    Display the version of the GitHub Actions Importer.
  configure  Start an interactive prompt to configure credentials used to authenticate with your CI server(s).
  audit      Plan your CI/CD migration by analyzing your current CI/CD footprint.
  forecast   Forecast GitHub Actions usage from historical pipeline utilization.
  dry-run    Convert a pipeline to a GitHub Actions workflow and output its yaml file.
  migrate    Convert a pipeline to a GitHub Actions workflow and open a pull request with the changes.
```

### Configuration

New versions of the GitHub Actions Importer are released on a regular basis. To ensure you're always up to date, the following command should be run often:

```bash
$ gh actions-importer update
```

**Note**: You will need to be authenticated with the GitHub Container Registry for this command to be successful. Optionally, credentials can be provided to this command that will be used to authenticate on your behalf:

```bash
$ echo $GITHUB_TOKEN | gh actions-importer update --username $GITHUB_HANDLE --password-stdin
```

In order for the GitHub Actions Importer to communicate with your current CI/CD server and GitHub, various credentials must be available for the command. These can be configured using environment variables or a `.env.local` file. These environment variables can be configured in an interactive prompt by running the following command:

```bash
$ gh actions-importer configure
? Enter value for 'GITHUB_ACCESS_TOKEN' (leave empty to skip): 
...
```

You can find detailed information about using environment variables in the documentation that is available once you are granted access.

### Usage

Now that the GitHub Actions Importer is configured and up-to-date, different subcommands of `gh actions-importer` can be used to migrate to GitHub Actions.

#### Audit

The `audit` subcommand can be used to plan your CI/CD migration by analyzing your current CI/CD footprint. This analysis can then be used to plan timelines for migrating to GitHub Actions.

To run an audit, use the following command to determine the options that are relevant to your use case:

```bash
$ gh actions-importer audit -h
Description:
  Plan your CI/CD migration by analyzing your current CI/CD footprint.

[...]

Commands:
  azure-devops  An audit will output a list of data used in an Azure DevOps instance.
  circle-ci     An audit will output a list of data used in a CircleCI instance.
  gitlab        An audit will output a list of data used in a GitLab instance.
  jenkins       An audit will output a list of data used in a Jenkins instance.
  travis-ci     An audit will output a list of data used in a Travis CI instance.
```

You can find detailed information about running an audit with the GitHub Actions Importer in the documentation that is available once you are granted access to the public preview.

#### Forecast

The `forecast` subcommand can be used to forecast GitHub Actions usage based on historical pipeline usage.

To run a forecast, use the following command to determine the options that are relevant to you:

```bash
$ gh actions-importer forecast -h
Description:
  Forecasts GitHub Actions usage from historical pipeline utilization.

[...]

Commands:
  azure-devops  Forecasts GitHub Actions usage from historical Azure DevOps pipeline utilization.
  jenkins       Forecasts GitHub Actions usage from historical Jenkins pipeline utilization.
  gitlab        Forecasts GitHub Actions usage from historical GitLab pipeline utilization.
  circle-ci     Forecasts GitHub Actions usage from historical CircleCI pipeline utilization.
  travis-ci     Forecasts GitHub Actions usage from historical Travis CI pipeline utilization.
  github        Forecasts GitHub Actions usage from historical GitHub pipeline utilization.
```

You can find detailed information about running a forecast with the GitHub Actions Importer in the documentation that is available once you are granted access to the public preview.

#### Dry run

The `dry-run` subcommand can be used to convert a pipeline to its GitHub Actions equivalent and write the workflow to your local filesystem.

To run a dry run, use the following command to determine the options that are relevant to you:

```bash
$ gh actions-importer dry-run -h
Description:
  Convert a pipeline to a GitHub Actions workflow and output its yaml file.

[...]

Commands:
  azure-devops  Convert an Azure DevOps pipeline to a GitHub Actions workflow and output its yaml file.
  circle-ci     Convert a CircleCI pipeline to GitHub Actions workflows and output the yaml file(s).
  gitlab        Convert a GitLab pipeline to a GitHub Actions workflow and output the yaml file.
  jenkins       Convert a Jenkins job to a GitHub Actions workflow and output its yaml file.
  travis-ci     Convert a Travis CI pipeline to a GitHub Actions workflow and output its yaml file.
```

You can find detailed information about running a dry run with the GitHub Actions Importer in the documentation that is available once you are granted access to the public preview.

#### Migrate

The `migrate` subcommand can be used to convert a pipeline to its GitHub Actions equivalent and then create a pull request with the contents.

To run a migration, use the following command to determine the options that are relevant to your use case:

```bash
$ gh actions-importer migrate -h
Description:
  Convert a pipeline to a GitHub Actions workflow and open a pull request with the changes.

[...]

Commands:
  azure-devops  Convert an Azure DevOps pipeline to a GitHub Actions workflow and open a pull request with the changes.
  circle-ci     Convert a CircleCI pipeline to GitHub Actions workflows and open a pull request with the changes.
  gitlab        Convert a GitLab pipeline to a GitHub Actions workflow and open a pull request with the changes.
  jenkins       Convert a Jenkins job to a GitHub Actions workflow and open a pull request with the changes.
  travis-ci     Convert a Travis CI pipeline to a GitHub Actions workflow and and open a pull request with the changes.
```

You can find detailed information about running a migration with the GitHub Actions Importer in the documentation that is available once you are granted access to the public preview.
