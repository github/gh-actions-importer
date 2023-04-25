# Apt

## Travis Input

```yaml
apt:
  packages:
  - cmake
```

### Transformed Github Action

```yaml
- run: apt-get -y install cmake
```

### Unsupported Options

- sources
- config
- dist
