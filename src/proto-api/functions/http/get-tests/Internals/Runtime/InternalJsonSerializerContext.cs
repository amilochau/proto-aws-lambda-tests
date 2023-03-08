using System.Text.Json.Serialization;

namespace Milochau.Proto.Http.GetTests.Internals.Runtime
{
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(ExceptionInfo))]
    [JsonSerializable(typeof(StatusResponse))]
    public partial class InternalJsonSerializerContext : JsonSerializerContext
    {
    }
}
