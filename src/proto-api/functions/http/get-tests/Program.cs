using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
//using Amazon.DynamoDBv2;
//using Amazon.DynamoDBv2.Model;
using Milochau.Proto.Http.GetTests.DataAccess;
using Milochau.Proto.Http.GetTests.Internals;
using Milochau.Proto.Shared.Entities;

namespace Milochau.Proto.Http.GetTests
{
    public class Function
    {
        private static async Task Main()
        {
            var handlerWrapper = HandlerWrapper.GetHandlerWrapper(FunctionHandler,
                deserializerInput: stream => JsonSerializer.Deserialize(stream, ApplicationJsonSerializerContext.Default.APIGatewayHttpApiV2ProxyRequest),
                serializeOutput: (output, stream) => JsonSerializer.Serialize(stream, output, ApplicationJsonSerializerContext.Default.APIGatewayHttpApiV2ProxyResponse));

            using var httpClient = new HttpClient(new SocketsHttpHandler()) { Timeout = TimeSpan.FromHours(12) };

            var lambdaBootstrap = new LambdaBootstrap(httpClient, handlerWrapper.Handler);
            await lambdaBootstrap.RunAsync();
        }

        public static async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest? request, Internals.Context.ILambdaContext context)
        {
            try
            {
                var cancellationToken = CancellationToken.None;
                if (request == null)
                {
                    throw new Exception("Request can not be deserialized");
                }

                //using var dynamoDBClient = new AmazonDynamoDBClient();
                var dynamoDbDataAccess = new DynamoDbDataAccess();// dynamoDBClient);

                return await DoAsync(request, context, dynamoDbDataAccess, cancellationToken);
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Error during test {ex.Message} {ex.StackTrace}");
                return HttpResponse.InternalServerError();
            }
        }

        public static async Task<APIGatewayHttpApiV2ProxyResponse> DoAsync(APIGatewayHttpApiV2ProxyRequest request, Internals.Context.ILambdaContext context, IDynamoDbDataAccess dynamoDbDataAccess, CancellationToken cancellationToken)
        {
            /*if (!request.TryParseAndValidate<GetTestsRequest>(new ValidationOptions { AuthenticationRequired = false }, out var proxyResponse, out var requestData))
            {
                return proxyResponse;
            }
            */

            // Store unsubscribe
            var requestData = new GetTestsRequest();
            var test = await dynamoDbDataAccess.StoreThenFetchTest(requestData, context.Logger, cancellationToken);
            return HttpResponse.Ok(test, ApplicationJsonSerializerContext.Default.Test);
        }
    }

    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
    [JsonSerializable(typeof(GetTestsRequest))]
    [JsonSerializable(typeof(Test))]
    public partial class ApplicationJsonSerializerContext : JsonSerializerContext
    {
    }
}