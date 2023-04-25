# Parameter Definitions Property

## Designer Pipeline

### Jenkins Input

```xml
   <parameterDefinitions>
      <hudson.model.StringParameterDefinition>
         <name>WORST_GAME_OF_THRONES_CHARACTER</name>
         <description />
         <defaultValue>jorah.mormont</defaultValue>
         <trim>false</trim>
      </hudson.model.StringParameterDefinition>
      <hudson.model.StringParameterDefinition>
         <name>sha1</name>
         <description>"The git ref (branch, tag, commit) to build"</description>
         <defaultValue>develop</defaultValue>
         <trim>false</trim>
      </hudson.model.StringParameterDefinition>
      <hudson.model.BooleanParameterDefinition>
         <name />
         <description />
         <defaultValue>false</defaultValue>
      </hudson.model.BooleanParameterDefinition>
      <hudson.plugins.copyartifact.BuildSelectorParameter plugin="copyartifact@1.45.1">
         <name />
         <description />
         <defaultSelector class="hudson.plugins.copyartifact.StatusBuildSelector" />
      </hudson.plugins.copyartifact.BuildSelectorParameter>
      <hudson.scm.CvsTagsParamDefinition plugin="cvs@2.16">
         <name />
         <cvsRoot />
         <password>{AQAAABAAAAAQK7zHOsUkJq5e8bQo3PFrjNmprZXh2MVtOiAWNPRptwk=}</password>
         <moduleName />
         <passwordRequired>false</passwordRequired>
      </hudson.scm.CvsTagsParamDefinition>
      <hudson.model.ChoiceParameterDefinition>
         <name />
         <description />
         <choices class="java.util.Arrays$ArrayList">
            <a class="string-array">
               <string />
            </a>
         </choices>
      </hudson.model.ChoiceParameterDefinition>
      <com.cloudbees.plugins.credentials.CredentialsParameterDefinition plugin="credentials@2.3.13">
         <name />
         <description />
         <defaultValue />
         <credentialType>com.cloudbees.plugins.credentials.common.StandardCredentials</credentialType>
         <required>false</required>
      </com.cloudbees.plugins.credentials.CredentialsParameterDefinition>
      <com.cwctravel.hudson.plugins.extended__choice__parameter.ExtendedChoiceParameterDefinition plugin="extended-choice-parameter@0.82">
         <name />
         <description />
         <quoteValue>false</quoteValue>
         <saveJSONParameterToFile>false</saveJSONParameterToFile>
         <visibleItemCount>5</visibleItemCount>
         <multiSelectDelimiter>,</multiSelectDelimiter>
      </com.cwctravel.hudson.plugins.extended__choice__parameter.ExtendedChoiceParameterDefinition>
      <hudson.model.FileParameterDefinition>
         <name />
         <description />
      </hudson.model.FileParameterDefinition>
      <net.uaznia.lukanus.hudson.plugins.gitparameter.GitParameterDefinition plugin="git-parameter@0.9.13">
         <name />
         <description />
         <uuid>c21432c5-9ff3-406b-884a-83ae0d8a6825</uuid>
         <type>PT_TAG</type>
         <branch />
         <tagFilter>*</tagFilter>
         <branchFilter>.*</branchFilter>
         <sortMode>NONE</sortMode>
         <defaultValue />
         <selectedValue>NONE</selectedValue>
         <quickFilterEnabled>false</quickFilterEnabled>
         <listSize>5</listSize>
      </net.uaznia.lukanus.hudson.plugins.gitparameter.GitParameterDefinition>
      <hudson.plugins.jira.listissuesparameter.JiraIssueParameterDefinition plugin="jira@3.1.1">
         <name />
         <description />
         <jiraIssueFilter />
         <altSummaryFields />
      </hudson.plugins.jira.listissuesparameter.JiraIssueParameterDefinition>
      <hudson.plugins.jira.versionparameter.JiraVersionParameterDefinition plugin="jira@3.1.1">
         <name />
         <description />
         <projectKey />
         <showReleased>false</showReleased>
         <showArchived>false</showArchived>
      </hudson.plugins.jira.versionparameter.JiraVersionParameterDefinition>
      <hudson.model.TextParameterDefinition>
         <name />
         <description />
         <defaultValue />
         <trim>false</trim>
      </hudson.model.TextParameterDefinition>
      <hudson.model.PasswordParameterDefinition>
         <name />
         <description />
         <defaultValue>{AQAAABAAAAAQ0HyV0HXfAOQgA6btGfX3/a8iIJiNqQH9kiY7xVotwTU=}</defaultValue>
      </hudson.model.PasswordParameterDefinition>
      <hudson.model.RunParameterDefinition>
         <name />
         <description />
         <projectName />
         <filter>ALL</filter>
      </hudson.model.RunParameterDefinition>
      <hudson.model.StringParameterDefinition>
         <name />
         <description />
         <defaultValue />
         <trim>false</trim>
      </hudson.model.StringParameterDefinition>
      <com.synopsys.arc.jenkinsci.plugins.customtools.versions.ToolVersionParameterDefinition plugin="custom-tools-plugin@0.7">
         <name>Version 1</name>
         <description>This is the first version</description>
         <toolName>NodeJS</toolName>
      </com.synopsys.arc.jenkinsci.plugins.customtools.versions.ToolVersionParameterDefinition>
   </parameterDefinitions>
</hudson.model.ParametersDefinitionProperty>
```

### Transformed Github Action

```yaml
on:
  workflow_dispatch:
    inputs:
      WORST_GAME_OF_THRONES_CHARACTER:
        required: false
        default: jorah.mormont
```

### Unsupported Options

- Boolean Parameter
- Build Selector for Copy Artifact
- CVS Symbolic Name Parameter
- Choice Parameter
- Credentials Parameter
- Extended Choice Parameter
- File Parameter
- Git Parameter
- Jira Issue Parameter
- Jira Version Parameter
- Multi-line String Parameter
- Password Parameter
- Run Parameter
- String Parameter
- Tool Version

## Jenkinsfile Pipeline

### Jenkins Input

```groovy
parameters {
   string(name: 'PERSON', defaultValue: 'Mr Jenkins', description: 'Who should I say hello to?')

   text(name: 'BIOGRAPHY', defaultValue: '', description: 'Enter some information about the person')

   booleanParam(name: 'TOGGLE', defaultValue: true, description: 'Toggle this value')

   choice(name: 'CHOICE', choices: ['One', 'Two', 'Three'], description: 'Pick something')

   password(name: 'PASSWORD', defaultValue: 'SECRET', description: 'Enter a password')
}
```

### Transformed Github Action

```yaml
on:
  workflow_dispatch:
    inputs:
      PERSON:
        required: false
        description: Who should I say hello to?
        default: Mr Jenkins
```

### Unsupported Options

- Boolean Parameter
- Build Selector for Copy Artifact
- CVS Symbolic Name Parameter
- Choice Parameter
- Credentials Parameter
- Extended Choice Parameter
- File Parameter
- Git Parameter
- Jira Issue Parameter
- Jira Version Parameter
- Multi-line String Parameter
- Password Parameter
- Run Parameter
- Tool Version
