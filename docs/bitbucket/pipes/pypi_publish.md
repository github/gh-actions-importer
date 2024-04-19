# Pypi Publish

[BitBucket Pypi Publish Documentation](https://bitbucket.org/atlassian/pypi-publish)

## Bitbucket Input

```yaml
- pipe: atlassian/pypi-publish:0.4.0
  variables:
    PYPI_USERNAME: $PYPI_USERNAME
    PYPI_PASSWORD: $PYPI_PASSWORD
    DISTRIBUTIONS: 'bdist_wheel'
    REPOSITORY: 'https://test.pypi.org/legacy/'
    FOLDER: 'myfolder'
```

## Transformed GitHub Action
```yaml
- uses: actions/setup-python@v4.7.0
- name: Build Distributions
  run: python setup.py bdist_wheel
  working-directory: myfolder
- uses: pypa/gh-action-pypi-publish@v1.8.10
  with:
    user: "${{ secrets.PYPI_USERNAME }}"
    password: "${{ secrets.PYPI_PASSWORD }}"
    repository-url: https://test.pypi.org/legacy/
    packages-dir: myfolder
```

## Unsupported Options
* DEBUG
