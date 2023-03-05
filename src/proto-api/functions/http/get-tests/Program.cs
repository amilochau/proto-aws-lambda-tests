using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
//using Amazon.DynamoDBv2;
//using Amazon.DynamoDBv2.Model;
//using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
//using Milochau.Core.Aws.ApiGateway;
using Milochau.Proto.Http.GetTests.DataAccess;
using Milochau.Proto.Shared.Entities;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]
namespace Milochau.Proto.Http.GetTests
{
    public class Function
    {
        private static async Task Main()
        {
            Func<string, ILambdaContext, Task<string>> handler = FunctionHandler;
            //Func<APIGatewayHttpApiV2ProxyRequest, ILambdaContext, Task<APIGatewayHttpApiV2ProxyResponse>> handler = FunctionHandler;
            await LambdaBootstrapBuilder.Create(handler, new SourceGeneratorLambdaJsonSerializer<ApplicationJsonSerializerContext>())
                .Build()
                .RunAsync();
        }

        public static async Task<string> FunctionHandler(string request, ILambdaContext context)
        //public static async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
        {
            try
            {
                var cancellationToken = CancellationToken.None;

                //using var dynamoDBClient = new AmazonDynamoDBClient();
                var dynamoDbDataAccess = new DynamoDbDataAccess();// dynamoDBClient);

                return await DoAsync(request, context, dynamoDbDataAccess, cancellationToken);
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Error during test {ex.Message} {ex.StackTrace}");
                return "ko"; // HttpResponse.InternalServerError();
            }
        }

        public static async Task<string> DoAsync(string request, ILambdaContext context, IDynamoDbDataAccess dynamoDbDataAccess, CancellationToken cancellationToken)
        //public static async Task<APIGatewayHttpApiV2ProxyResponse> DoAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context, IDynamoDbDataAccess dynamoDbDataAccess, CancellationToken cancellationToken)
        {
            /*if (!request.TryParseAndValidate<GetTestsRequest>(new ValidationOptions { AuthenticationRequired = false }, out var proxyResponse, out var requestData))
            {
                return proxyResponse;
            }
            */

            // Store unsubscribe
            var requestData = new GetTestsRequest();
            var test = await dynamoDbDataAccess.StoreThenFetchTest(requestData, context.Logger, cancellationToken);
            return "ok";// HttpResponse.Ok(test, ApplicationJsonSerializerContext.Default.Test);
        }
    }

    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    //[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
    //[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
    [JsonSerializable(typeof(IEnumerable<string>))]
    [JsonSerializable(typeof(GetTestsRequest))]
    [JsonSerializable(typeof(Test))]
    //[JsonSerializable(typeof(GetItemRequest))]
    public partial class ApplicationJsonSerializerContext : JsonSerializerContext
    {
    }
}