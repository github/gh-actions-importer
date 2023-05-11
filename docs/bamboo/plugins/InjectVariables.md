# Inject variables

## Bamboo input

```yaml
- inject-variables:
    file: ./test.txt
    scope: LOCAL
    namespace: inject
```

## Transformed Github Action

```yaml
- run: |-
    while IFS= read -r line || [[ -n "$line" ]]; do
      if [[ -n "$line" ]]; then
        key=$(echo "inject_$line" | cut -d= -f1)
        value=$(echo "$line" | cut -d= -f2)
        echo "$key=$value" >> "$GITHUB_ENV"
      fi
    done < ./test.txt
  shell: bash
```

## Unsupported Options

- Result Scope:  The injected environment variables will be scoped to the current job only.
For more information about variable scoping in GitHub Actions see the docs [here](https://docs.github.com/en/actions/learn-github-actions/variables).
