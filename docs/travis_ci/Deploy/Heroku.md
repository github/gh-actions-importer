# Heroku

## Travis Input

```yaml
deploy:
  provider: heroku
  username: <username>
  password: <encrypted password>
  edge: true
```

## Transformed Github Action

```yaml
    - run: |-
        cat > ~/.netrc <<EOF
                          machine api.heroku.com
                            login $HEROKU_EMAIL
                            password $HEROKU_API_KEY
                          machine git.heroku.com
                            login $HEROKU_EMAIL
                            password $HEROKU_API_KEY
                        EOF
      env:
        HEROKU_API_KEY: "${{ secrets.HEROKU_API_KEY }}"
        HEROKU_EMAIL: "${{ secrets.HEROKU_EMAIL }}"
      if: "${{ github.event_name == 'push' && github.ref == 'refs/heads/main' }}"
    - run: heroku git:remote --app ${{ github.event.repository.name }}
      if: "${{ github.event_name == 'push' && github.ref == 'refs/heads/main' }}"
    - run: git push heroku main
      if: "${{ github.event_name == 'push' && github.ref == 'refs/heads/main' }}"
```

### Unsupported Options

- git
- strategy
- cleanup
- skip cleanup (deprecated in Travis)
