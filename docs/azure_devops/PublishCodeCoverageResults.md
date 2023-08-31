# Publish Code Coverage Results Task

## Azure DevOps Input

```yaml
- task: PublishCodeCoverageResults@1
  inputs: 
    codeCoverageTool: Cobertura
    summaryFileLocation: '$(System.DefaultWorkingDirectory)/**/*coverage.xml'
    pathToSources: '$(System.DefaultWorkingDirectory)/src/'
    failIfCoverageEmpty: true
    additionalCodeCoverageFiles: '$(System.DefaultWorkingDirectory)/**/results.xml'
```

## Transformed Github Action

```yaml
- name: Generate Coverage Report
  uses: danielpalme/ReportGenerator-GitHub-Action@4.8.12
  with:
    reports: "${{ github.workspace }}/**/*coverage.xml"
    sourcedirs: "${{ github.workspace }}/src/"
    targetdir: coveragereport_${{ github.run_number }}
    reporttypes: HtmlInline
  continue-on-error: false
- name: Upload CoverageReport
  uses: actions/upload-artifact@v2
  with:
    name: CoverageReport
    path: coveragereport_${{ github.run_number }}
- name: Upload CoverageReportFiles
  uses: actions/upload-artifact@v2
  with:
    name: CoverageReportFiles
    path: "${{ github.workspace }}/**/results.xml"
```

## Unsupported Inputs and Aliases
- reportDirectory
