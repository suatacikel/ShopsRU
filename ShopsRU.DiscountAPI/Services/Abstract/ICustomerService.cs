using ShopsRU.DiscountAPI.Dtos.Response;
using ShopsRU.DiscountAPI.Model;

namespace ShopsRU.DiscountAPI.Services.Abstract
{
    public interface ICustomerService
    {
        ResponseDto<Customer> GetById(int id);
    }
}
