# GitHub Valet CLI

[![.github/workflows/ci.yml](https://github.com/github/gh-valet/actions/workflows/ci.yml/badge.svg)](https://github.com/github/gh-valet/actions/workflows/ci.yml)

Valet helps facilitate the migration of Azure DevOps, CircleCI, GitLab CI, Jenkins, and Travis CI pipelines to GitHub Actions. This repository provides functionality that extends the [GitHub CLI](https://cli.github.com/) to migrate pipelines to GitHub Actions using Valet.

> Valet is currently private and customers must be onboarded prior to using the `gh-valet` CLI extension. Please reach out to [GitHub Sales](https://github.com/enterprise/contact) to inquire about being granted access.

**Note**: You can request support by creating an issue [here](https://github.com/github/gh-valet/issues/new?assignees=&labels=help+wanted&template=support.yml&title=%5BSupport%5D%3A+). The Valet team responds to support requests Monday through Friday between the hours of 9AM EST and 5PM PST.

## Supported platforms

Valet currently supports migrating pipelines to GitHub Actions from the following platforms:

- Azure DevOps
- CircleCI
- GitLab CI
- Jenkins
- Travis CI

You can find detailed information about how Valet works for each of the supported platforms in the documentation that is available once you are granted access.

## Getting started with the Valet CLI

Valet is distributed as a Docker container and this extension to the official [GitHub CLI](https://cli.github.com) to interact with the Docker container.

### Prerequisites

The following requirements must be met to be able to run Valet:

- The Docker CLI must be [installed](https://docs.docker.com/get-docker/) and running
- The official [GitHub CLI](https://cli.github.com) must be installed
- You must have credentials to [authenticate](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-container-registry#authenticating-to-the-container-registry) with the GitHub Container Registry after you are granted access.

### Installation

Next, the Valet CLI extension can be installed via this command:

```bash
$ gh extension install github/gh-valet
```

To verify the extension is installed, run this command:

```bash
$ gh valet -h
Description:
  Valet is a tool to help plan and facilitate migrations to GitHub Actions.

Options:
  -?, -h, --help  Show help and usage information

Commands:
  update    Update to the latest version of Valet
  version   Check the version of the Valet docker container.
  audit     An audit will output a list of data used in a CI/CD instance.
  dry-run   Convert a pipeline to a GitHub Actions workflow and output its yaml file.
  migrate   Convert a pipeline to a GitHub Actions workflow and open a pull request with the changes.
  forecast  Forecasts GitHub Actions usage from historical pipeline utilization.
```

### Configuration

New versions of Valet are released on a regular basis. To ensure you're always up to date, the following command should be run often:

```bash
$ gh valet update
```

**Note**: You will need to be authenticated with GitHub Container Registery for this command to be successful. Optionally, credentials can be provided to this command that will be used to authenticate on your behalf:

```bash
$ echo $GITHUB_TOKEN | gh valet update --username $GITHUB_HANDLE --password-stdin
```

In order for Valet to communicate with your current CI server and GitHub, various credentials must be available for the command. These can be configured using environment variables or a `.env.local` file. These environment variables can be configured in an interative prompt by running the following command:

```bash
$ gh valet configure
? Enter value for 'GITHUB_ACCESS_TOKEN' (leave empty to skip): 
...
```

You can find detailed information about using environment variables in the documentation that is available once you are granted access.

### Usage

Now that Valet is configured and up-to-date, different subcommands of `gh valet` can be used to facilate a migration to GitHub Actions.

#### Audit

The `audit` subcommand can be used to scan a CI server and output a summary of the current pipelines. This summary can then be used to plan timelines for migrating to GitHub Actions.

To run an audit, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet audit -h
Description:
  An audit will output a list of data used in a CI/CD instance.

<omitted for brevity>

Commands:
  azure-devops  An audit will output a list of data used in an Azure DevOps instance.
  circle-ci     An audit will output a list of data used in a CircleCI instance.
  gitlab        An audit will output a list of data used in a GitLab instance.
  jenkins       An audit will output a list of data used in a Jenkins instance.
  travis-ci     An audit will output a list of data used in a Travis CI instance.
```

You can find detailed information about running an audit with Valet in the documentation that is available once you are granted access.

#### Forecast

The `forecast` subcommand can be used to forecast GitHub Actions usage from historical pipeline usage.

To run a forecast, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet forecast -h
Description:
  Forecasts GitHub Actions usage from historical pipeline utilization.

<omitted for brevity>

Commands:
  azure-devops  Forecasts GitHub Actions usage from historical Azure DevOps pipeline utilization.
  jenkins       Forecasts GitHub Actions usage from historical Jenkins pipeline utilization.
  gitlab        Forecasts GitHub Actions usage from historical GitLab pipeline utilization.
  circle-ci     Forecasts GitHub Actions usage from historical CircleCI pipeline utilization.
  travis-ci     Forecasts GitHub Actions usage from historical Travis CI pipeline utilization.
  github        Forecasts GitHub Actions usage from historical GitHub pipeline utilization.
```

You can find detailed information about running a forecast with Valet in the documentation that is available once you are granted access.

#### Dry-run

The `dry-run` subcommand can be used to convert a pipeline to its GitHub Actions equivalent and write the workflow to your local filesystem.

To run a dry-run, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet dry-run -h
Description:
  Convert a pipeline to a GitHub Actions workflow and output its yaml file.

<omitted for brevity>

Commands:
  azure-devops  Convert an Azure DevOps pipeline to a GitHub Actions workflow and output its yaml file.
  circle-ci     Convert a CircleCI pipeline to GitHub Actions workflows and output the yaml file(s).
  gitlab        Convert a GitLab pipeline to a GitHub Actions workflow and output the yaml file.
  jenkins       Convert a Jenkins job to a GitHub Actions workflow and output its yaml file.
  travis-ci     Convert a Travis CI pipeline to a GitHub Actions workflow and output its yaml file.
```

You can find detailed information about running a dry-run with Valet in the documentation that is available once you are granted access.

#### Migrate

The `migrate` subcommand can be used to convert a pipeline to its GitHub Actions equivalent and then create a pull request with the contents.

To run a migration, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet migrate -h
Description:
  Convert a pipeline to a GitHub Actions workflow and open a pull request with the changes.

<omitted for brevity>

Commands:
  azure-devops  Convert an Azure DevOps pipeline to a GitHub Actions workflow and open a pull request with the changes.
  circle-ci     Convert a CircleCI pipeline to GitHub Actions workflows and open a pull request with the changes.
  gitlab        Convert a GitLab pipeline to a GitHub Actions workflow and open a pull request with the changes.
  jenkins       Convert a Jenkins job to a GitHub Actions workflow and open a pull request with the changes.
  travis-ci     Convert a Travis CI pipeline to a GitHub Actions workflow and and open a pull request with the changes.
```

You can find detailed information about running a migration with Valet in the documentation that is available once you are granted access.
