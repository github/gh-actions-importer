# SonarCloud

## Travis Input - Other(for JS YS, Go, Python, PHP...)

```yaml
addons:
  sonarcloud:
    organization: "mona_org"
    token:
      secure: "**************************"
script:
  - sonar-scanner
```

### Transformed Github Action

```yaml
- name: SonarCloud Scan
  uses: sonarsource/sonarcloud-github-action@v1.8
  env:
    SONAR_TOKEN: "${{ secrets.SONAR_TOKEN }}"
    GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
    SONARCLOUD_URL: "${{ secrets.SONAR_URL }}"
  with:
    args: |
      #WARNING: projectKey is a required field, ensure it is configured here or in your project file
      -Dsonar.projectKey='<<INSERT KEY HERE>>'/`
      -Dsonar.organization='MonaOrg'/`
      -Dsonar.host.url='${{ secrets.SONAR_URL }}'/`
      -Dsonar.login=${{ env.SONAR_TOKEN }}/`
```

## Travis Input - C, C++, or ObjC

```yaml
addons:
  sonarcloud:
    organization: "mona_org"
    token:
      secure: "**************************"
script:
  - build-wrapper-linux-x86-64 --out-dir bw-output <insert_your_clean_build_command>
  - sonar-scanner -Dsonar.cfamily.build-wrapper-output=bw-output
```

### Transformed Github Action

```yaml
- name: Download and set up build-wrapper
  env:
    BUILD_WRAPPER_DOWNLOAD_URL: "${{ env.SONAR_SERVER_URL }}/static/cpp/build-wrapper-linux-x86.zip"
  run: |-
    curl -sSLo $HOME/.sonar/build-wrapper-linux-x86.zip ${{ env.BUILD_WRAPPER_DOWNLOAD_URL }}/`
    unzip -o $HOME/.sonar/build-wrapper-linux-x86.zip -d $HOME/.sonar//`
    echo "$HOME/.sonar/build-wrapper-linux-x86" >> $GITHUB_PATH/`
- name: Download and set up sonar-scanner
  env:
    SONAR_SCANNER_DOWNLOAD_URL: https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-${{ env.SONAR_SCANNER_VERSION }}-linux.zip
    SONAR_SCANNER_VERSION: 4.4.0.2170
  run: |-
    mkdir -p $HOME/.sonar/`
    curl -sSLo $HOME/.sonar/sonar-scanner.zip ${{ env.SONAR_SCANNER_DOWNLOAD_URL }}/`
    unzip -o $HOME/.sonar/sonar-scanner.zip -d $HOME/.sonar//`
    echo "$HOME/.sonar/sonar-scanner-${{ env.SONAR_SCANNER_VERSION }}-linux/bin" >> $GITHUB_PATH/`
- name: SonarCloud Scan
  env:
    GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
    SONAR_TOKEN: "${{ secrets.SONAR_TOKEN }}"
  run: |
    sonar-scanner -Dsonar.cfamily.build-wrapper-output=bw-output/`
    #WARNING: projectKey is a required field, ensure it is configured here or in your project file
    -Dsonar.projectKey='<<INSERT KEY HERE>>'/`
    -Dsonar.organization='MonaOrg'/`
    -Dsonar.host.url='${{ secrets.SONAR_URL }}'/`
    -Dsonar.login=${{ env.SONAR_TOKEN }}/`
```

## Travis Input - Gradle

```yaml
addons:
  sonarcloud:
    organization: "mona_org"
    token:
      secure: "**************************"
script:
  - ./gradlew sonarqube
```

### Transformed Github Action

```yaml
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
- name: SonarCloud Scan
  env:
    GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
    SONAR_TOKEN: "${{ secrets.SONAR_TOKEN }}"
  run: |
    ./gradlew sonarqube/`
    #WARNING: projectKey is a required field, ensure it is configured here or in your project file
    -Dsonar.projectKey='<<INSERT KEY HERE>>'/`
    -Dsonar.organization='MonaOrg'/`
    -Dsonar.host.url='${{ secrets.SONAR_URL }}'/`
    -Dsonar.login=${{ env.SONAR_TOKEN }}/`
```

## Travis Input - Maven

```yaml
addons:
  sonarcloud:
    organization: "mona_org"
    token:
      secure: "**************************"
script:
  - mvn clean org.jacoco:jacoco-maven-plugin:prepare-agent install sonar:sonar -Dsonar.projectKey=important_project_jenkins
```

### Transformed Github Action

```yaml
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
- name: SonarCloud Scan
  env:
    GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
    SONAR_TOKEN: "${{ secrets.SONAR_TOKEN }}"
  run: |
    mvn clean org.jacoco:jacoco-maven-plugin:prepare-agent install sonar:sonar/`
    #WARNING: projectKey is a required field, ensure it is configured here or in your project file
    -Dsonar.projectKey='<<INSERT KEY HERE>>'/`
    -Dsonar.organization='MonaOrg'/`
    -Dsonar.host.url='${{ secrets.SONAR_URL }}'/`
    -Dsonar.login=${{ env.SONAR_TOKEN }}/`
```

## Travis Input - DotNet

```yaml
addons:
  sonarcloud:
    organization: "mona_org"
    token:
      secure: "**************************"
script:
  - mvn clean org.jacoco:jacoco-maven-plugin:prepare-agent install sonar:sonar -Dsonar.projectKey=important_project_jenkins
```

### Transformed Github Action

```yaml
- name: Set up JDK 1.11
  uses: actions/setup-java@v3.10.0
  with:
    java-version: '1.11'
- name: SonarCloud Scan
  env:
    GITHUB_TOKEN: "${{ secrets.GITHUB_TOKEN }}"
    SONAR_TOKEN: "${{ secrets.SONAR_TOKEN }}"
  run: |
    mvn clean org.jacoco:jacoco-maven-plugin:prepare-agent install sonar:sonar/`
    #WARNING: projectKey is a required field, ensure it is configured here or in your project file
    -Dsonar.projectKey='<<INSERT KEY HERE>>'/`
    -Dsonar.organization='MonaOrg'/`
    -Dsonar.host.url='${{ secrets.SONAR_URL }}'/`
    -Dsonar.login=${{ env.SONAR_TOKEN }}/`
```

### Unsupported Options

- DotNet projects
