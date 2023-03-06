terraform {
  backend "s3" {
    bucket = "terraform-shd-bucket"
    region = "eu-west-3"
    key    = "terraform.tfstate"

    workspace_key_prefix = "proto" # To adapt for new projects
  }

  required_providers {
    aws = {
      source = "hashicorp/aws"
    }
  }

  required_version = ">= 1.3.0"
}

provider "aws" {
  region = var.aws_provider_settings.region

  default_tags {
    tags = {
      application = var.conventions.application_name
      host        = var.conventions.host_name
    }
  }
}

module "checks" {
  source      = "git::https://github.com/amilochau/tf-modules.git//shared/checks?ref=v1"
  conventions = var.conventions
}

module "functions_app" {
  source      = "git::https://github.com/amilochau/tf-modules.git//aws/functions-app?ref=v1"
  conventions = var.conventions

  lambda_settings = {
    architecture = "x86_64"
    runtime      = "provided.al2"
    functions = {
      for k, v in var.lambda_settings.functions : "${replace(v.function_type, "/", "-")}-${k}" => {
        memory_size_mb        = v.memory_size_mb
        timeout_s             = v.timeout_s
        deployment_file_path  = "${var.lambda_settings.base_directory}/${v.function_type}/${k}/${v.package_file}"
        handler               = v.handler
        environment_variables = v.environment_variables
        http_triggers         = v.http_triggers
        sns_triggers          = v.sns_triggers
        ses_accesses          = v.ses_accesses
      }
    }
  }

  dynamodb_tables_settings = var.dynamodb_tables_settings
}
