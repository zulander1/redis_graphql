namespace GraphQL
{
    public class CustomerType : ObjectType<CustomerModel>
    {
        protected override void Configure(IObjectTypeDescriptor<CustomerModel> descriptor)
        {
            descriptor.Name("Customer");
            base.Configure(descriptor);
        }
    }
}