conventions = {
  application_name = "proto"
  host_name        = "dev"
}

lambda_settings = {
  base_directory = "../src/proto-api/functions"
  functions = {
    get-tests = {
      function_type = "http"
      http_triggers = [{
        method    = "GET"
        route     = "/tests"
        anonymous = true
      }]
    }
  }
}

dynamodb_tables_settings = {
  "tests" = {
    partition_key = "id"
    ttl = {
      enabled = true
    }
  }
}
