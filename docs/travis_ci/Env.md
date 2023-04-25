# Env

## Travis Input

```yaml
env:
  FOO: foo
```

```yaml
env:
- FOO: foo
```

```yaml
env:
- FOO=foo
```

```yaml
env: FOO=foo
```

```yaml
env:
- secure: encrypted string
```

```yaml
env:
  secure: encrypted string
```

Note: Secure environment variables will surface as a manual task so the user can add the secret in Actions.

```yaml
env:
  global:
  - FOO: foo
  jobs:
  - FOO: foo
  - FOO: bar
```

Note: Global environment variables to be defined on all jobs and Job environment variables will expand a matrix.

## Transformed Github Action

```yaml
env: 
  FOO: foo
```

### Supported Services

- Env maps
- Env sequence of maps
- Env strings
- Sequence of env strings
- Sequence of secure envs
- Secure envs
