using ShopsRU.DiscountAPI.Model;

namespace ShopsRU.DiscountAPI.Repositories.Abstract
{
    public interface ICustomerRepository
    {
        Customer GetById(int id);
    }
}
