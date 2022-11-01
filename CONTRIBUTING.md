## Contributing

We're thrilled that you'd like to contribute to this project. Your help is essential for keeping it great.

Contributions to this project are [released](https://docs.github.com/en/site-policy/github-terms/github-terms-of-service#6-contributions-under-repository-license) to the public under the [project's open source license](LICENSE).

Please note that this project is released with a [Contributor Code of Conduct](CODE_OF_CONDUCT.md). By participating in this project you agree to abide by its terms.

Here's some helpful notes on how to contribute to this project, including details on how to get started working the codebase.

## How to submit a bug or request a feature

If you think you've found a bug or have a great idea for new functionality please create an issue in this repo.

## How to provide feedback or ask for help

Use the [Discussions](https://github.com/github/gh-actions-importer/discussions) tab in this repo for more general feedback or any questions/comments on this tooling.

## Configure your development environment

To get started, you'll need [.NET Core 6.0](https://dotnet.microsoft.com/en-us/download) installed on your local machine.

The solution can be built using the following command:

```bash
$ dotnet build src/ActionsImporter.sln
```

Unit tests can be run using the following command:

```bash
$ dotnet test src/ActionsImporter.sln
```

Code linting can be run using the following command:

```bash
$ dotnet format src/ActionsImporter.sln
```

## Submitting a Pull Request

Before submitting a Pull Request please first open an issue to get feedback on the change you intend to submit.
