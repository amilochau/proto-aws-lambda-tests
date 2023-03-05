# Terraform code

Here are a few commands that can be useful to work with Terraform. Please use them from the current directory.

| Command | Description |
| ------- | ----------- |
| `terraform init` | Initialization |
| `terraform get` | Get latest version of the module |
| `terraform workspace list` | List available workspaces |
| `terraform workspace new WORKSPACE_NAME` | Create a new workspace (as `dev`, `prd`, `shd`) |
| `terraform workspace set WORKSPACE_NAME` | Set the current workspace |
| `terraform plan -var-file="hosts/HOST_NAME.tfvars"` | Plan the deployment of the host |
| `terraform apply -var-file="hosts/HOST_NAME.tfvars"` | Apply the deployment of the host |
