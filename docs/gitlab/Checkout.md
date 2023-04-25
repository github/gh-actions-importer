# Checkout

## GitLab Input

- Checking out source code is done implicitly.
- In variables, you can set `GIT_STRATEGY: none` or `GIT_CHECKOUT: "false"` to skip checking out source code.
- Git submodules can be declared in variables: `GIT_SUBMODULE_STRATEGY: none, normal or recursive`
- In Settings, CI/CD, General pipelines you can set the Git shallow clone (`ci_default_git_depth`), which defaults to 50.
  - In variables, fetch depth can be overridden by setting `GIT_DEPTH: "5"`
- Using Git LFS client you can configure Large File Storage(`lfs`), which defaults to `true`.

```yaml

checkout-always:
  stage: build
  script:
    - echo "Job will include an actions/checkout@v2 with default Gitlab fields"
skip-checkout:
  stage: build
  variables:
    GIT_STRATEGY: none
  script: echo "Job will not include a checkout step"
git-submodule:
  variables:
    GIT_SUBMODULE_STRATEGY: recursive # or normal
  stage: build
  script: echo "Job will include a checkout step with submodules"
git-strategy-checkout:
  variables:
    GIT_STRATEGY: clone # or normal
    GIT_CHECKOUT: "false"
  stage: build
  script: echo "Job will not include a checkout step"
git-checkout:
  variables:
    GIT_CHECKOUT: "false"
  stage: build
  script: echo "Job will not include"
git-depth:
  variables:
    GIT_DEPTH: "7"
  stage: build
  script: echo "Will override settings fetch depth"

```

### Transformed Github Action

```yaml

checkout-always:
  runs-on: ubuntu-latest
  steps:
  - uses: actions/checkout@v2
    with:
      fetch-depth: 50
      lfs: true
  - run: echo "Job will include an actions/checkout@v2 with default Gitlab fields"
skip-checkout:
  runs-on: ubuntu-latest
  env:
    GIT_STRATEGY: none
  steps:
  - run: echo "Job will not include a checkout step"
git-submodule:
  runs-on: ubuntu-latest
  env:
    GIT_SUBMODULE_STRATEGY: recursive
  steps:
  - uses: actions/checkout@v2
    with:
      fetch-depth: 50
      lfs: true
      submodules: recursive
  - run: echo "Job will include a checkout step with submodules"
git-strategy-checkout:
  runs-on: ubuntu-latest
  env:
    GIT_STRATEGY: clone
    GIT_CHECKOUT: 'false'
  steps:
  - run: echo "Job will not include a checkout step"
git-checkout:
  runs-on: ubuntu-latest
  env:
    GIT_CHECKOUT: 'false'
  steps:
  - run: echo "Job will not include"
git-depth:
  runs-on: ubuntu-latest
  env:
    GIT_DEPTH: '7'
  steps:
  - uses: actions/checkout@v2
    with:
      fetch-depth: '7'
      lfs: true
  - run: echo "Will override settings fetch depth"
```

### Unsupported Options

- GIT_CLEAN_FLAGS
- GIT_FETCH_EXTRA_FLAGS
