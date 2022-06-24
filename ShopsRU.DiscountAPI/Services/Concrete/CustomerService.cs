using ShopsRU.DiscountAPI.Dtos.Response;
using ShopsRU.DiscountAPI.Model;
using ShopsRU.DiscountAPI.Repositories.Abstract;
using ShopsRU.DiscountAPI.Services.Abstract;
using System.Net;

namespace ShopsRU.DiscountAPI.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ResponseDto<Customer> GetById(int id)
        {
            return ResponseDto<Customer>.Success(_customerRepository.GetById(id), (int)HttpStatusCode.OK);
        }
    }
}
