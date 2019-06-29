# Tools: DynamoDB - Terraform

## Overview

The following instructions will get you started with creating the relevant AWS DynamoDB tables for IdentityServer4.Contrib.AwsDynamoDB

For further reading, see

- Terraform Developer Documentation, https://www.terraform.io/docs/index.html
- Terraform AWS Provider, https://www.terraform.io/docs/providers/aws/index.html
- Terraform AWS Provider DynamoDB, https://www.terraform.io/docs/providers/aws/r/dynamodb_table.html

## Installation

### Download executable
https://www.terraform.io/downloads.html

### Docker Image
https://hub.docker.com/r/hashicorp/terraform/

## Usage
### Initialize Infrastructure State
```
> ./terraform init
```

### Run Execution Plan
```
> ./terraform plan \
    -var-file="./configuration/terraform.tfvars" \
    -var-file="./configuration/jd-env.tfvars"
```

### Terraform Environment
```
> ./terraform apply \
    -var-file="./configuration/terraform.tfvars" \
    -var-file="./configuration/jd-env.tfvars"
```

### Destroy Environment
```
> ./terraform destroy \
    -var-file="./configuration/terraform.tfvars" \
    -var-file="./configuration/jd-env.tfvars"
```

## License
MIT