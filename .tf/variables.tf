variable "conventions" {
  description = "Conventions to use"
  type = object({
    application_name = string
    host_name        = string
  })
}

variable "aws_provider_settings" {
  description = "Settings to configure the AWS provider"
  type = object({
    region = optional(string, "eu-west-3")
  })
  default = {}
}

variable "lambda_settings" {
  description = "Lambda settings"
  type = object({
    base_directory = string
    functions = map(object({
      function_type         = string
      memory_size_mb        = optional(number, 128)
      timeout_s             = optional(number, 10)
      package_source_file   = optional(string, "bin/Release/net7.0/linux-x64/publish/bootstrap")
      package_file          = optional(string, "bin/Release/net7.0/linux-x64/publish/bootstrap.zip")
      handler               = optional(string, "bootstrap")
      environment_variables = optional(map(string), {})
      http_triggers = optional(list(object({
        method      = string
        route       = string
        anonymous   = optional(bool, false)
        enable_cors = optional(bool, false)
      })), [])
      sns_triggers = optional(list(object({
        topic_name = string
      })), [])
      ses_accesses = optional(list(object({
        domain = string
      })), [])
    }))
  })
}

variable "dynamodb_tables_settings" {
  description = "Settings to configure DynamoDB tables for the API"
  type = map(object({
    partition_key = string
    sort_key      = optional(string, null)
    attributes = optional(map(object({
      type = string
    })), {})
    ttl = optional(object({
      enabled        = bool
      attribute_name = optional(string, "ttl")
      }), {
      enabled = false
    })
    global_secondary_indexes = optional(map(object({
      partition_key      = string
      sort_key           = string
      non_key_attributes = list(string)
    })), {})
  }))
  default = {}
}
