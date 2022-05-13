# GitHub Valet CLI

[![.github/workflows/ci.yml](https://github.com/github/gh-valet/actions/workflows/ci.yml/badge.svg)](https://github.com/github/gh-valet/actions/workflows/ci.yml)

Valet helps facilitate the migration of Azure DevOps, CircleCI, GitLab CI, Jenkins, and Travis CI pipelines to GitHub Actions. This repository provides functionality that extends the [GitHub CLI](https://cli.github.com/) to migrate pipelines to GitHub Actions using Valet.

> Because Valet is in private preview, customers must be onboarded prior to using the Valet IssueOps workflow. Please reach out to GitHub Sales to inquire about being added to the private preview.

Note: You can request support by creating an issue [here](https://github.com/github/gh-valet/issues/new). The Valet team responds to support requests Monday through Friday between the hours of 9AM EST and 5PM PST.

## Supported platforms

Valet currently supports migrating pipelines to GitHub Actions from the following platforms:

- Azure DevOps
- CircleCI
- GitLab CI
- Jenkins
- Travis CI

Learn more about how Valet works for each of the supported platforms in the documentation [here](https://github.com/valet-customers/distribution/blob/main/README.md).

## Getting started with the Valet CLI

### Installation

First, you'll need to download the official [GitHub CLI](https://cli.github.com).

Next, the Valet CLI extension can be installed via this command:

```bash
$ gh extension install github/gh-valet
```

To verify the extension is installed, run this command:

```bash
$ gh valet -h
Description:
  Valet is a tool to help plan and facilitate migrations to GitHub Actions.

Usage:
  gh-valet [command] [options]

Options:
  -?, -h, --help  Show help and usage information

Commands:
  update    Update to the latest version of Valet
  version   Check the version of the Valet docker container.
  audit     An audit will output a list of data used in a CI/CD instance.
  dry-run   Convert a pipeline to a GitHub Actions workflow and output its yaml file.
  migrate   Convert a pipeline to a GitHub Actions workflow and open a pull request with the changes.
  forecast  Forecasts GitHub actions usage from historical pipeline utilization.
```

### Configuration

New versions of Valet are released on a regular basis. To ensure you're always up to date, the following command should be run often:

```bash
$ gh valet update
```

**Note**: You will need to be authenticated with GitHub Container Registery for this command to be successful. Optionally, credentials can be provided to this command that will be used to authenticate on your behalf:

```bash
$ gh valet update --username $GITHUB_HANDLE --password $GITHUB_TOKEN
```

In order for Valet to communicate with your current CI server and GitHub, various credentials must be available for the command. These can be configured using environment variables or a `.env.local` file as described [here](https://github.com/valet-customers/distribution/blob/main/README.md#using-environment-variables).

### Usage

Now that Valet is configured and up-to-date, different subcommands of `gh valet` can be used to facilate a migration to GitHub Actions.

#### Audit

The `audit` subcommand can be used to scan a CI server and output a summary of the current pipelines. This summary can then be used to plan timelines for migrating to GitHub Actions.

To run an audit, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet audit -h
Description:
  An audit will output a list of data used in a CI/CD instance.

Usage:
  gh-valet audit [command] [options]

<omitted for brevity>

Commands:
  azure-devops  An audit will output a list of data used in an Azure DevOps instance.
  circle-ci     An audit will output a list of data used in a CircleCI instance.
  gitlab        An audit will output a list of data used in a GitLab instance.
  jenkins       An audit will output a list of data used in a Jenkins instance.
  travis-ci     An audit will output a list of data used in a Travis CI instance.
```

Detailed documentation about running an audit with Valet can be found [here](https://github.com/valet-customers/distribution/blob/main/README.md#audit).

#### Forecast

The `forecast` subcommand can be used to forecast GitHub Actions usage from historical pipeline usage.

To run a forecast, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet forecast -h
Description:
  Forecasts GitHub actions usage from historical pipeline utilization.

Usage:
  gh-valet forecast [command] [options]

<omitted for brevity>

Commands:
  azure-devops  Forecasts GitHub Actions usage from historical Azure DevOps pipeline utilization.
```

Detailed documentation about running a forecast with Valet can be found [here](https://github.com/valet-customers/distribution/blob/main/README.md#forecast).

#### Dry-run

The `dry-run` subcommand can be used to convert a pipeline to its GitHub Actions equivalent and write the workflow to your local filesystem.

To run a dry-run, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet dry-run -h
Description:
  Convert a pipeline to a GitHub Actions workflow and output its yaml file.

Usage:
  gh-valet dry-run [command] [options]

<omitted for brevity>

Commands:
  azure-devops  Convert an Azure DevOps pipeline to a GitHub Actions workflow and output its yaml file.
  circle-ci     Convert a CircleCI pipeline to GitHub Actions workflows and output the yaml file(s).
  gitlab        Convert a GitLab pipeline to a GitHub Actions workflow and output the yaml file.
  jenkins       Convert a Jenkins job to a GitHub Actions workflow and output its yaml file.
  travis-ci     Convert a Travis CI pipeline to a GitHub Actions workflow and output its yaml file.
```

Detailed documentation about running a dry-run with Valet can be found [here](https://github.com/valet-customers/distribution/blob/main/README.md#dry-run).

#### Migrate

The `migrate` subcommand can be used to convert a pipeline to its GitHub Actions equivalent and then create a pull request with the contents.

To run a migration, use the following command to determine the options that are relevant to your use case:

```bash
$ gh valet migrate -h
Description:
  Convert a pipeline to a GitHub Actions workflow and open a pull request with the changes.

Usage:
  gh-valet migrate [command] [options]

<omitted for brevity>

Commands:
  azure-devops  Convert an Azure DevOps pipeline to a GitHub Actions workflow and open a pull request with the changes.
  circle-ci     Convert a CircleCI pipeline to GitHub Actions workflows and open a pull request with the changes.
  gitlab        Convert a GitLab pipeline to a GitHub Actions workflow and open a pull request with the changes.
  jenkins       Convert a Jenkins job to a GitHub Actions workflow and open a pull request with the changes.
  travis-ci     Convert a Travis CI pipeline to a GitHub Actions workflow and and open a pull request with the changes.
```

Detailed documentation about running a migration with Valet can be found [here](https://github.com/valet-customers/distribution/blob/main/README.md#migrate).
