using System;

namespace Milochau.Proto.Shared.Entities
{
    public class Test
    {
        public const string TableNameSuffix = "tests";
        public const int TtlDurationInDays = 365;

        public string Id { get; set; } = null!;
        public DateTimeOffset Ttl { get; set; }
        public DateTimeOffset Creation { get; set; }
    }
}
