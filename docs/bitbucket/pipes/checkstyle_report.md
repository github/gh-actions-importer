# Checkstyle Report

[BitBucket Checkstyle Report Documentation](https://bitbucket.org/atlassian/checkstyle-report/src/master/)

## Bitbucket Input

```yaml
pipe: atlassian/checkstyle-report:0.4.0
variables:
  CHECKSTYLE_REPORT_TITLE: 'My Report'
  CHECKSTYLE_RESULT_PATTERN: '.*/checkstyle-result.xml$'
  CHECKSTYLE_REPORT_ID: '123'
  REPORT_FAIL_SEVERITY: 'info'
```

## Transformed GitHub Action
```yaml
uses: jwgmeligmeyling/checkstyle-github-action@v1.2
with:
  path: ".*/checkstyle-result.xml$"
  title: My Report
```

## Unsupported Options
* `REPORT_FAIL_SEVERITY`
* `CHECKSTYLE_REPORT_ID`
