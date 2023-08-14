using HotChocolate.Data;
using HotChocolate.Resolvers;
using Redis.OM;
using Redis.OM.Searching;
using System.Linq.Expressions;

namespace GraphQL
{
    [ExtendObjectType("Query")]
    public class Query
    {
        private readonly RedisCollection<CustomerModel> _customers;

        public Query(RedisConnectionProvider redisConnectionProvider)
        {
            _customers = (RedisCollection<CustomerModel>)redisConnectionProvider.RedisCollection<CustomerModel>();
        }

        [UseOffsetPaging()]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<CustomerModel> GetCustomer(IResolverContext context)
        {
            return _customers.AsQueryable().Project(context);
        }
    }
}