﻿using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Circle;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "circle-ci";
    protected override string Description => "Convert a CircleCI pipeline to GitHub Actions workflows and output the yaml file(s).";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Organization,
        Common.Project,
        Common.Provider,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceGitHubAccessToken,
        Common.SourceGitHubInstanceUrl,
        Common.SourceFilePath
    );
}
