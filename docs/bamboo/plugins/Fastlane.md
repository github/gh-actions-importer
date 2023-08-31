# Fastlane

**Note:** `fastlane` can be installed in multiple ways. This installation follows the preferred method of using Bundler. It is expected that the repository has a `Gemfile` that includes the `fastlane` gem.

For more information consult the `fastlane` [documentation](https://docs.fastlane.tools/getting-started/ios/setup/).

## Bamboo input

```yaml
- any-task:
    plugin-key: com.atlassian.bamboo.plugins.xcode.bamboo-xcode-plugin:fastlaneTaskType
    configuration:
      environmentVariables: BAR=bar BAZ=baz
      label: fastlane
      lane: deploy submit:false build_number:24
      workingSubDirectory: tmp
    conditions:
      - variable:
          exists: ABC
    description: Automation with fastlane
```

## Transformed GitHub Action

```yaml
- uses: ruby/setup-ruby@v1.146.0
  with:
    bundler-cache: true
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
- name: Automation with fastlane
  run: bundle exec fastlane deploy submit:false build_number:24
  working-directory: tmp
  if: env.ABC != ''
  env:
    BAR: bar
    BAZ: baz
```

## Unsupported Options

- none
