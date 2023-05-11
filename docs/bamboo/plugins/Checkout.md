# Checkout

## Bamboo input

```yaml
- checkout:
    path: ./default
    force-clean-build: true
    description: Checkout Default Repository
- checkout:
    repository: Test-Repo
    path: ./test
    force-clean-build: true
    description: Checkout Default Repository
```

## Transformed Github Action

```yaml
- uses: actions/checkout@v3.5.0
  with:
    path: "./default"
- uses: actions/checkout@v3.5.0
  env:
    REPO_NAME: "**org/name**"
  with:
    repository: "${{ env.REPO_NAME }}"
    path: "./test"
```

## Unsupported Options
- none
