# AntExec Builder

## Designer Pipeline

### Jenkins Input

```xml
<hudson.plugins.antexec.AntExec plugin="antexec@1.11">
  <scriptSource>&lt;echo&gt;foo&lt;/echo&gt;</scriptSource>
  <extendedScriptSource>&lt;echo&gt;bar&lt;/echo&gt;</extendedScriptSource>
  <scriptName>some_script.xml</scriptName>
  <properties>bar=baz</properties>
  <antOpts>-Xms2G -Xmx2G</antOpts>
  <keepBuildfile>false</keepBuildfile>
  <verbose>false</verbose>
  <emacs>true</emacs>
  <noAntcontrib>false</noAntcontrib>
</hudson.plugins.antexec.AntExec>
```

### Transformed Github Action

```yaml
- name: Run Ant Exec command
  env:
    ANT_OPTS: "-Xms2G -Xmx2G"
  shell: bash
  run: |-
    cat > some_script.xml <<EOF
    <?xml version="1.0" encoding="utf-8"?>
      <project default="some_script.xml" xmlns:antcontrib="antlib:net.sf.antcontrib" basedir=".">
      <property file="some_script.xml.properties"/>
      <property environment="env"/>
      <target name="some_script.xml">
      <echo>foo</echo>
      </target>
      <echo>bar</echo>
      </project>
    EOF
    cat > some_script.xml.properties <<EOF
    bar=baz
    EOF
    ant -file some_script.xml -emacs
    rm some_script.xml.properties
```

### Unsupported Options

- verbose
- noAntcontrib
