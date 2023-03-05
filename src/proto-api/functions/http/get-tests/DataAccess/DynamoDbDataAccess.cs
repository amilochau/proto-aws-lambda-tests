using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Milochau.Proto.Shared.Entities;
using Milochau.Core.Aws.DynamoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.Proto.Http.GetTests.DataAccess
{
    public interface IDynamoDbDataAccess
    {
        Task<Test> StoreThenFetchTest(GetTestsRequest request, CancellationToken cancellationToken);
    }

    public class DynamoDbDataAccess : IDynamoDbDataAccess
    {
        public static string ConventionsPrefix { get; set; } = Environment.GetEnvironmentVariable("CONVENTION__PREFIX")!;

        private readonly IAmazonDynamoDB amazonDynamoDB;

        public DynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public async Task<Test> StoreThenFetchTest(GetTestsRequest request, CancellationToken cancellationToken)
        {
            // Store
            var test = new Test
            {
                Id = Guid.NewGuid().ToString("N"),
                Creation = DateTimeOffset.Now,
                Ttl = DateTimeOffset.Now.AddDays(Test.TtlDurationInDays),
            };

            await amazonDynamoDB.PutItemAsync(new PutItemRequest
            {
                TableName = $"{ConventionsPrefix}-table-{Test.TableNameSuffix}",
                Item = new Dictionary<string, AttributeValue>()
                .Append("id", test.Id)
                .Append("creation", test.Creation)
                .Append("ttl", test.Ttl)
                .ToDictionary(x => x.Key, x => x.Value)
            }, cancellationToken);

            // Fetch
            var dynamoDbResponse = await amazonDynamoDB.GetItemAsync(new GetItemRequest
            {
                TableName = $"{ConventionsPrefix}-table-{Test.TableNameSuffix}",
                Key = new Dictionary<string, AttributeValue>()
                    .Append("id", test.Id)
                    .ToDictionary(x => x.Key, x => x.Value),
                ProjectionExpression = DynamoDbExpressionsHelpers.BuildProjectionExpression(new[]{ "id" }),
                ExpressionAttributeNames = DynamoDbExpressionsHelpers.BuildExpressionAttributeNames(new[]{ "id" }),
            }, cancellationToken);

            var item = dynamoDbResponse.Item;

            return new Test
            {
                Id = item.ReadString("id"),
                Creation = item.ReadDateTimeOffset("creation"),
                Ttl = item.ReadDateTimeOffset("ttl"),
            };
        }
    }
}
