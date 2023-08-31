# PythonScript Task

## Azure DevOps Input

```yaml
# Python script
# Run a Python file or inline script
- task: PythonScript@0
  inputs:
    #scriptSource: 'filePath' # Options: filePath, inline
    #scriptPath: # Required when scriptSource == filePath
    #script: # Required when scriptSource == inline
    #arguments: # Optional
    #pythonInterpreter: # Optional
    #workingDirectory: # Optional
    #failOnStderr: false # Optional```

### Transformed Github Action

```yaml
- name: inline with arguments
  run: |-
    import sys
    print ("hello i''m an inline script")
    print('Number of arguments: {}'.format(len(sys.argv)))
    print('Argument(s) passed: {}'.format(str(sys.argv)))
  shell: python {0} one two three

- name: pythong file with arguments
  run: python cleese.py one two three
```

### Unsupported Inputs

- failOnStderr
