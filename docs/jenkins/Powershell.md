# Powershell

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.powershell.PowerShell plugin="powershell@1.4">
  <command>Set-Content -Path "C:\temp\$($env:Filename).txt" -Value $env:Message</command>
  <configuredLocalRules/>
  <useProfile>true</useProfile>
  <stopOnError>true</stopOnError>
</hudson.plugins.powershell.PowerShell>
```

### Transformed Github Action

```yaml
name:  "powershell",
run:   "Set-Content -Path \"C:\\temp\\$($env:Filename).txt\" -Value $env:Message",
shell: "powershell"
```

### Unsupported Options

- None

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
steps {
    powershell(script:"echo 'param world'", label:"test-label", returnStdout: false)
    powershell 'echo "Hello World"'
    powershell '''
                echo "Multiline shell steps works too"
                Remove-Item 'pyFolder' -Recurse
                    mkdir pyFolder
                    ls
                    $items = @('one','two','three')
                    foreach ($item in $items)
                    {
                        echo $item
                    }
                    '''
}
```

### Transformed Github Action

```yaml
# Single/Inline
name: powershell
shell: powershell
run: echo "Hello World"

# Multi-line
name: powershell
shell: powershell
run: |2

echo "Multiline shell steps works too"
Remove-Item 'pyFolder' -Recurse
mkdir pyFolder
ls
$items = @('one','two','three')
foreach ($item in $items)
{
  echo $item
}
```

### Unsupported Options

- returnStatus
