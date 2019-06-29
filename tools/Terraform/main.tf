#
# Entry Point
#
provider "aws" {
    region = "${var.aws_region}"
    shared_credentials_file = "${var.aws_credentials_file}"
    profile = "${var.aws_credentials_profile}"
}

module "DynamoDB" {
  source = "./modules/DynamoDB"
  
  prefix = "${var.environment}"
}
