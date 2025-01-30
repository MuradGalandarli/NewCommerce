using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Persistence.Context;
using NewCommerce.Persistence.Repositoryes;


namespace NewCommerce.Persistence.Repositoryes
{
    public class CustomerReadRepository:ReadRepository<Customer>, ICustomerReadRepository
    {
        public NewCommerceDb _context;
        public CustomerReadRepository(NewCommerceDb context):base(context) {
        { }

    }
    }
}
