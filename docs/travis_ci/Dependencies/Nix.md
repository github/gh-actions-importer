# Nix

## Travis Input

```yaml
nix: 2.3.6
```

## Transformed Github Action

```yaml
- uses: cachix/install-nix-action@v19
  with:
    install_url: https://nixos.org/releases/nix/nix-2.3.6/install
```
