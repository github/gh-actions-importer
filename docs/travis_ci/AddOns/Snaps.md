# Snaps

## Travis Input

```yaml
snaps:
- name: foo
  classic: true
  channel: edge
```

### Transformed Github Action

```yaml
- run: sudo snap install foo --classic --channel=edge
```
