# CircleCI/Slack Notify

## CircleCI Input

```yaml
orbs:
  slack: circleci/slack@x.y
jobs:
  slack-example:
    docker:
      - image: 'cimg/base:stable'
    steps:
      - slack/notify:
          branch_pattern: ^feature-.+$
          channel: my-channel
          event: fail
          ignore_errors: true
          tag_pattern: ^tag-.+$

```

### Transformed Github Action
#### Composite Action Feature: enabled
workflow usage
```yaml
steps:
- uses: ./.github/actions/circleci_slack_notify
  continue-on-error: true
  if: failure()
  with:
    slack_webhook: "${{ secrets.SLACK_WEBHOOK }}"
    slack_channel: my-channel
    branch_pattern: "^feature-.+$"
    tag_pattern: "^tag-.+$"
```

action.yml
```yaml
name: circleci_slack_notify
inputs:
  slack_webhook:
    required: true
  slack_channel:
    required: true
  branch_pattern:
    required: false
    default: ".+"
  tag_pattern:
    required: false
    default: ".+"
runs:
  using: composite
  steps:
  - id: branch_pattern
    shell: bash
    run: |-
      if [[ '${{ env.VALUE_TO_MATCH }}' =~ ${{ env.BRANCH_PATTERN }} ]]; then
        echo "match=true" >> $GITHUB_OUTPUT
      fi
    env:
      VALUE_TO_MATCH: "${{ github.ref }}"
      BRANCH_PATTERN: "${{ inputs.branch_pattern }}"
  - id: tag_pattern
    shell: bash
    run: |-
      if [[ '${{ env.VALUE_TO_MATCH }}' =~ ${{ env.TAG_PATTERN }} ]]; then
        echo "match=true" >> $GITHUB_OUTPUT
      fi
    env:
      VALUE_TO_MATCH: "${{ github.ref }}"
      TAG_PATTERN: "${{ inputs.tag_pattern }}"
  - uses: rtCamp/action-slack-notify@v2.2.0
    if: "${{ steps.branch_pattern.outputs.match == 'true' && steps.tag_pattern.outputs.match == 'true' }}"
    env:
      SLACK_WEBHOOK: "${{ inputs.slack_webhook }}"
      SLACK_CHANNEL: "${{ inputs.slack_channel }}"
```

#### Composite Action Feature: disabled
```yaml
steps:
- id: branch_pattern
  shell: bash
  run: |-
    if [[ '${{ env.VALUE_TO_MATCH }}' =~ ${{ env.BRANCH_PATTERN }} ]]; then
      echo "match=true" >> $GITHUB_OUTPUT
    fi
  env:
    VALUE_TO_MATCH: "${{ github.ref }}"
    BRANCH_PATTERN: "^feature-.+$"
- id: tag_pattern
  shell: bash
  run: |-
    if [[ '${{ env.VALUE_TO_MATCH }}' =~ ${{ env.TAG_PATTERN }} ]]; then
      echo "match=true" >> $GITHUB_OUTPUT
    fi
  env:
    VALUE_TO_MATCH: "${{ github.ref }}"
    TAG_PATTERN: "^tag-.+$"
- uses: rtCamp/action-slack-notify@v2.2.0
  if: "${{ failure() && steps.branch_pattern.outputs.match == 'true' && steps.tag_pattern.outputs.match == 'true' }}"
  env:
    SLACK_WEBHOOK: "${{ secrets.SLACK_WEBHOOK }}"
    SLACK_CHANNEL: my-channel
  continue-on-error: true
```
### Unsupported Options

- custom
- mentions
- template
- debug
