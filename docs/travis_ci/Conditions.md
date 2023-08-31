# Conditions

## Travis Input

```yaml
if: "branch = master AND type = cron OR tag = bar"
```

## Transformed Github Action

```yaml
if: ${{ github.ref == 'refs/heads/master' && github.event_name == 'schedule' || github.ref == 'refs/tags/bar' }}
```

### Supported Options

- These attributes: (type, repo, branch, tag, sender, head_branch, os)
- Individual terms
- Comparing values
- Comparing function calls to attributes
- Comparing function calls
- Attributes in lists
- Predicates
- Nested function calls
- Boolean operators
- Parenthesis
- We support all keywords being case-insensitive
- We recognize Travis aliases (eg. "! is an alias to NOT")
- Line continuation (multiline conditions)

### Unsupported Options

- Matching against regular expressions
- Function calls in lists
- Nested env function calls (eg. env(env(FOO)))
- These attributes: (commit_message, fork, head_repo, language, sudo, dist, group)
