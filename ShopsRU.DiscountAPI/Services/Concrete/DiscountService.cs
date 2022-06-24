using ShopsRU.DiscountAPI.Common.Constants;
using ShopsRU.DiscountAPI.Common.Enums;
using ShopsRU.DiscountAPI.Dtos.Response;
using ShopsRU.DiscountAPI.Model;
using ShopsRU.DiscountAPI.Repositories.Abstract;
using ShopsRU.DiscountAPI.Services.Abstract;
using System.Net;

namespace ShopsRU.DiscountAPI.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;

        public DiscountService(IDiscountRepository discountRepository, IInvoiceService invoiceService, ICustomerService customerService)
        {
            _discountRepository = discountRepository;
            _invoiceService = invoiceService;
            _customerService = customerService;
        }

        public ResponseDto<decimal> Calculate(int invoiceId)
        {
            var invoice = _invoiceService.GetById(invoiceId);
            if (invoice == null || !invoice.IsSuccessful)
                throw new ArgumentException(Messages.InvoiceNotFound);

            var customer = _customerService.GetById(invoice.Data.CustomerId);
            if (customer == null || !customer.IsSuccessful)
                throw new ArgumentException(Messages.CustomerNotFound);

            var customerDiscountTypes = getCustomerDiscountTypes(customer.Data);

            var customerDiscounts = _discountRepository.GetByTypes(customerDiscountTypes);

            var validDiscounts = customerDiscounts.Where(d => d.isValidInvoiceType(invoice.Data.Type)).ToList();

            decimal priceDiscount = 0;
            validDiscounts.ForEach(d => priceDiscount += d.CalcDiscountPrice(invoice.Data.Amount));

            return ResponseDto<decimal>.Success(invoice.Data.Amount - priceDiscount, (int)HttpStatusCode.OK);
        }

        #region private methods
        private List<DiscountTypes> getCustomerDiscountTypes(Customer customer)
        {
            var discountTypes = new List<DiscountTypes>();

            if (customer.Type == CustomerTypes.Employee)
            {
                discountTypes.Add(DiscountTypes.Percent30);
            }
            else if (customer.Type == CustomerTypes.Member)
            {
                discountTypes.Add(DiscountTypes.Percen10);
            }
            else if (customer.Type == CustomerTypes.Customer && customer.CreatedAt.AddYears(2) < DateTime.Now)
            {
                discountTypes.Add(DiscountTypes.Percent5);
            }

            discountTypes.Add(DiscountTypes.Discount5ForEvery100);

            return discountTypes;
        }
        #endregion
    }
}
