# Power Shell On Target Machines Task

## Azure DevOps Input

```yaml
steps:
- task: PowerShellOnTargetMachines@3
  displayName: 'Run PowerShell on $(machineNames)'
  inputs:
    Machines: 'test-vm1.westus.cloudapp.azure.com:5986'
    UserName: 'admin'
    UserPassword: '$(sysPassword)'
    ScriptType: FilePath
    ScriptPath: 'C:\Scripts\MainScript.ps1'
    ScriptArguments: '-p1 $A -p2 $B -p3 $C'
    InitializationScript: 'C:\Scripts\InitializationScript.ps1'
    NewPsSessionOptionArguments: '-SkipCACheck -IdleTimeout 7200000 -OperationTimeout 0 -OutputBufferingMode Block'
    failOnStderr: true
    ignoreLASTEXITCODE: true
    WorkingDirectory: 'C:\Scripts'
  continueOnError: true
```

## Transformed Github Action

```yaml
name: PowerShellOnTargetMachines-Classic
on:
  workflow_dispatch:
env:
  machineName: test-vm1.westus.cloudapp.azure.com:5986
  sysAccount: admin
  system_debug: 'false'
jobs:
  Job_1:
    name: Agent job 1
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v2
    - name: Run PowerShell on ${{ env.machineNames }}
      continue-on-error: true
      uses: Azure/login@v1
      with:
        creds: "${{ secrets.AZURE_CREDENTIALS }}"
        enable-AzPSSession: true
    - name: Run PowerShell on ${{ env.machineNames }}
      continue-on-error: true
      uses: azure/powershell@v1
      env:
        ACCOUNT_NAME: admin
        MACHINE_NAMES: vm1.westus.cloudapp.azure.com:5986
      with:
        inlineScript: |
          $securePwd =  ConvertTo-SecureString -String ${{ secrets.sysPassword }} -AsPlainText -Force
          $cred = New-Object System.Management.Automation.PSCredential("${{ env.SYSACCOUNT }}", $securePwd)
          $sessionOptions = New-PSSessionOption -SkipCACheck -IdleTimeout 7200000 -OperationTimeout 0 -OutputBufferingMode Block
          $scriptBlock = [scriptblock]::Create(
          {
              cd C:\Scripts
              C:\Scripts\InitializationScript.ps1
              C:\Scripts\MainScript.ps1 -p1 $A -p2 $B -p3 $C
          })
          $machineNames = "${{ env.MACHINENAMES }}".split(",")
          foreach($machine in $machineNames)
          {
            $spec = $machine.split(":")
            $fqdn = $spec[0]
            $port = [String]::IsNullOrWhiteSpace($spec[1]) ? "5986" : $spec[1]
            $session = New-PSSession -ComputerName $fqdn -port $port -Credential $cred -useSSL -SessionOption $sessionOptions
            Invoke-Command -Session $session -ScriptBlock $scriptBlock
          }
      errorActionPreference: Stop
      failOnStandardError: false
      azPSVersion: latest
```

### Unsupported Inputs

- AuthenticationMechanism
- SessionVariables

