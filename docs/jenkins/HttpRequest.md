# Http Request

## Designer Pipeline

### Jenkins Input

```xml
<jenkins.plugins.http__request.HttpRequest plugin="http_request@1.8.26">
  <url>https://jenkout.westus2.cloudapp.azure.com/job/freestyle-goat/</url>
  <ignoreSslErrors>true</ignoreSslErrors>
  <httpMode>GET</httpMode>
  <httpProxy/>
  <passBuildParameters>false</passBuildParameters>
  <validResponseCodes>100:399</validResponseCodes>
  <validResponseContent/>
  <acceptType>NOT_SET</acceptType>
  <contentType>NOT_SET</contentType>
  <outputFile/>
  <timeout>0</timeout>
  <consoleLogResponseBody>false</consoleLogResponseBody>
  <quiet>false</quiet>
  <authentication/>
  <requestBody/>
  <uploadFile/>
  <multipartName/>
  <wrapAsMultipart>false</wrapAsMultipart>
  <useSystemProperties>false</useSystemProperties>
  <customHeaders class="empty-list"/>
</jenkins.plugins.http__request.HttpRequest>
```

### Transformed Github Action

```yaml
name: "http request"
uses: "CamiloGarciaLaRotta/watermelon-http-client@v1.6"
with:
  method: "GET"
  url: "https://jenkout.westus2.cloudapp.azure.com/job/freestyle-goat/"
  data: "data"
  headers: "{ 'Content-Type': 'application/json','Accept': 'text/plain','Authorization': 'bearer ${{ secrets.BOT_TOKEN }}' }"
env:
  HTTPS_PROXY: my_proxy
```

### Unsupported Options

- ignoreSslErrors
- passBuildParameters
- validResponseCodes (unless it is [200:300])
- outputFile
- timeout
- consoleLogResponseBody
- quiet
- uploadFile
- multipartName
- wrapAsMultipart
- useSystemProperties

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
stage('Example Deploy') {
    steps {
        httpRequest('http://www.example.com') {
            httpMode('POST')
        }
    }
}
```

### Transformed Github Action

```yaml
jobs:
  Example-Deploy:
    name: Example Deploy
    steps:
    - uses: CamiloGarciaLaRotta/watermelon-http-client@v1.7
       with:
         url: http://www.example.com
         method: POST
```

### Unsupported Options

- consoleLogResponseBody
- ignoreSslErrors
- multipartName
- outputFile
- quiet
- responseHandle
- timeout
- uploadFile
- useSystemProperties
- validResponseCodes
- validResponseContent
- wrapAsMultipart
