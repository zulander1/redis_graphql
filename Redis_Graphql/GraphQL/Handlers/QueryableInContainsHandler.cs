using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;
 using System.Linq.Expressions;
using System.Reflection;

namespace GraphQL
{
    public class QueryableContainsHandler : QueryableStringOperationHandler
    {
        public QueryableContainsHandler(InputParser inputParser) : base(inputParser)
        {
        }

        protected override int Operation => DefaultFilterOperations.In;

        public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object? parsedValue)
        {
            Expression property = context.GetInstance();

            if (parsedValue is not null)
            {
                MethodInfo contains = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(x => x.Name.Contains(nameof(Enumerable.Contains))).Single(x => x.GetParameters().Length == 2).MakeGenericMethod(property.Type);
                return Expression.Call(contains, Expression.Constant(parsedValue), property);
            }
            throw new Exception();
        }
    }
}