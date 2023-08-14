using Redis.OM.Modeling;

namespace GraphQL
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { Prefix }, IndexName = Prefix + ":idx")]
    public class Customer
    {
        public const string Prefix = "BTS:EmployeeService:Employee";

        [Indexed][RedisIdField] public int Id { get; set; }
        [Indexed] public string FirstName { get; set; }
        [Indexed] public string LastName { get; set; }
    }
}