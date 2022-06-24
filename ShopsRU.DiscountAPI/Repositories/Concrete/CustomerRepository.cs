using ShopsRU.DiscountAPI.Common.MockDatas;
using ShopsRU.DiscountAPI.Model;
using ShopsRU.DiscountAPI.Repositories.Abstract;
using System.Linq;


namespace ShopsRU.DiscountAPI.Repositories.Concrete
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetById(int id)
        {

            return new MockData().Customers().FirstOrDefault(d => d.Id == id);
        }
    }
}
