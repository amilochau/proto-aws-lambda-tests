using Amazon.Lambda.APIGatewayEvents;
using Milochau.Core.Aws.ApiGateway;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Milochau.Proto.Http.GetTests
{
    public class GetTestsRequest : IParsableAndValidatable<GetTestsRequest>
    {
        public static bool TryParse(APIGatewayHttpApiV2ProxyRequest request, [NotNullWhen(true)] out GetTestsRequest? result)
        {
            result = new GetTestsRequest();
            return true;
        }

        public void Validate(Dictionary<string, Collection<string>> modelStateDictionary)
        {
        }
    }
}
