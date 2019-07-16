#
# Variables
#
variable "environment" {
  type = "string"
  default = ""
}

variable "aws_region" {
  type = "string"
}

variable "aws_credentials_file" {
  type = "string"
}

variable "aws_credentials_profile" {
  type = "string"
  default = "default"
}
