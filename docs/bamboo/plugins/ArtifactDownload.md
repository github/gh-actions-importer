# Artifact Download

## Bamboo input

```yaml
tasks:
- artifact-download:
    artifacts:
    - {}
    source-plan: IN-COM
- artifact-download:
    artifacts:
    - destination: /tmp
      name: my-artifacts
    - destination: /bin
      name: Binaries
    source-plan: IN-COM
```

## Transformed Github Action

```yaml
steps:
# warning: This will download all available artifacts, ensure this is the correct behavior.
- uses: actions/download-artifact@v3.0.1
- uses: actions/download-artifact@v3.0.1
  with:
    name: IN-COM_my-artifacts
    path: "/tmp"
- uses: actions/download-artifact@v3.0.1
  with:
    name: IN-COM_Binaries
    path: "/bin"
```

## Unsupported Options

- none
