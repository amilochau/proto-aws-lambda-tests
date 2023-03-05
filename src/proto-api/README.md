# Lambda functions code

Here are a few commands that can be useful to work with `dotnet lambda`. Please use them from the root directory.

| Command | Description |
| ------- | ----------- |
| `Get-ChildItem -Path .\src\emails-api\functions -Recurse -Filter *.csproj | ForEach-Object { $p = "$($_.Directory.FullName)/dist/function.zip"; if (Test-Path "$($p)") { Remove-Item "$($p)" }; dotnet lambda package -pl "$($_.Directory.FullName)" -o "$($p)" --disable-interactive true --msbuild-parameters -p:BuildSource=AwsCmd }` | Create zip package for deployment |
