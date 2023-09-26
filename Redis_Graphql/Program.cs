using AutoMapper;
using GraphQL;
using HotChocolate.Data.Filters.Expressions;
using Redis.OM;
using System.Linq.Expressions;
using System.Reflection;
using static StackExchange.Redis.Role;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
.AddGraphQLServer().AddProjections()
    .AddFiltering(x => x.AddDefaults()
    .AddProviderExtension(new QueryableFilterProviderExtension(f => f.AddFieldHandler<QueryableContainsHandler>())))
    .AddQueryType(d => d.Name("Query"))
        .AddType<Query>()
        .AddType<CustomerType>();

var provider = new RedisConnectionProvider("redis://localhost:6379");

builder.Services.AddSingleton(provider);

provider.Connection.CreateIndex(typeof(CustomerModel));

var customers = provider.RedisCollection<CustomerModel>();

var mapperConfig = new MapperConfiguration(f => f.AddProfile(new CustomerDTOProfile()));

builder.Services.AddSingleton(mapperConfig);
// Insert customer
customers.Insert(new CustomerModel()
{
    Id = 1,
    FirstName = "James",
    LastName = "Bond",
});

customers.Insert(new CustomerModel()
{
    Id = 2,
    FirstName = "Dr",
    LastName = "No",
});

var app = builder.Build();

app.MapGraphQL();

app.Run();

public class CustomerDTOProfile : Profile
{
    public CustomerDTOProfile()
    {
        CreateMap<CustomerModel, CustomerDTO>();
    }
}