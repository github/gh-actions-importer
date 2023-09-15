# Heroku Deploy

[BitBucket Heroku Deploy Documentation](https://bitbucket.org/atlassian/heroku-deploy)

## Bitbucket Input

```yaml
pipe: atlassian/heroku-deploy
variables:
  HEROKU_API_KEY: '<api_key>'
  HEROKU_APP_NAME: '<app_name>'
```

## Transformed GitHub Action
```yaml
 uses: AkhileshNS/heroku-deploy@v3.12.14
 with:
   heroku_api_key: "${{ secret.HEROKU_API_KEY }}"
   heroku_app_name: <app_name>
   heroku_email: "${{ env.HEROKU_EMAIL }}"
```

## Unsupported Options
- ACTION
- ZIP_FILE
- WAIT
- CONFIG_VARS
- DEBUG
