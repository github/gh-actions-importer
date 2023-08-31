# Azure Services Security Status Task

## Azure DevOps Input

```yaml
- task: AzSKSVTs@4
  inputs:
    ConnectedServiceNameARM: 'az-service-connection'
    GenerateMethodParameterSetSelection: 'TagNameValuePair'
    TagName: 'ScanMe'
    TagValue: 'yes'
    SubscriptionId: '123-456-786-1010'
    EnableGSSscan: true
    EnableOMSLogging: false
```

## Transformed Github Action

```yaml
- uses: Azure/login@v1
  with:
    creds: "${{ secrets.AZURE_CREDENTIALS }}"
    enable-AzPSSession: true
# Warning: This will force the installation of AzSK's dependencies, including the `az` module
- name: Install Secure DevOps Kit for Azure
  shell: pwsh
  run: Install-Module AzSK -Scope CurrentUser -AllowClobber -Force
# Warning: this is a simplified implementation of the AzSKSVTs build task and ignores any custom scanning capabilities.
- name: Run AzSK Security Scans
  shell: pwsh
  env:
    SUBSCRIPTION_ID: 123-456-786-1010
    TAG_NAME: ScanMe
    TAG_VALUE: 'Yes'
  run: |-
    Set-AzSKPrivacyNoticeResponse -AcceptPrivacyNotice Yes
    $scanResults = @()
    $failures = @()
    $scanResults += Get-AzSKAzureServicesSecurityStatus -s "${{ env.SUBSCRIPTION_ID }}" -dnof -tgn "${{ env.TAG_NAME }}" -tgv "${{ env.TAG_VALUE }}" -ExcludeTags "OwnerAccess,RBAC"
    $scanResults += Get-AzSKSubscriptionSecurityStatus -s "${{ env.SUBSCRIPTION_ID }}" -dnof -ExcludeTags "OwnerAccess,GraphRead"
    $passingStatuses = @("Passed")
    if($Env:TREATASPASSED -ne $null){
      $passingStatuses += $Env:TREATASPASSED -split ","
    }
    foreach($result in $scanResults) {
      $csv = Get-ChildItem -Path $result -Filter *.csv
      $failures += Import-Csv -Path $csv | where Status -notin $passingStatuses
    }
    if ($failures.count -gt 0){
      Write-Output "::error::$($failures.count) failures were found during the Security Scan(s), see end of log for details"
      Write-Output $failures | Format-Table
      exit 1
    }
```

## Unsupported Inputs and Aliases
- Send events to Log Analytics
- Aggregate control status
- Do not auto-update AzSK
- Custom scanning capabilites set in pipelin variables
