# Delete Directory

## Designer pipeline

This plugin is not supported in Designer pipelines.

## Jenkinsfile pipeline

### Jenkins input

```groovy
// Deletes current directory recursively.
steps {
  deleteDir()
}
// Wrapping deleteDir in a dir step deletes the specified directory.
steps {
    dir("/root") {
        deleteDir()
    }
}

```

### Transformed Github Action

```yaml
- name: delete directory
  shell: bash
  run: rm -rf "`pwd`"
```

```yaml
- name: delete directory
  shell: bash
  run: rm -rf "`pwd`"
  working-directory: "/root"
```

### Unsupported Options

- None
