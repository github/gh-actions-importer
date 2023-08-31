# Scp

## Bamboo input

```yaml
- scp:
    host: "example.com\r\nlocalhost"
    local-path: ./important.txt
    use-ant-pattern: 'true'
    destination-path: tmp
    authentication:
      username: foo
      password: BAMSCRT@0@0@1y8Jmz6IIfriZO7UfsIOFg==
    port: '23'
    description: SCP With Password
```

## Transformed Github Action

```yaml
- name: SCP With Password(example.com)
      uses: appleboy/scp-action@v0.1.4
      with:
        host: example.com
        port: '23'
        source: "./important.txt"
        target: tmp
        username: foo
        password: "${{ secrets.FOO_PASSWORD }}"
- name: SCP With Password(localhost)
  uses: appleboy/scp-action@v0.1.4
  with:
    host: localhost
    port: '23'
    source: "./important.txt"
    target: tmp
    username: foo
    password: "${{ secrets.FOO_PASSWORD }}"
```

## Unsupported Options