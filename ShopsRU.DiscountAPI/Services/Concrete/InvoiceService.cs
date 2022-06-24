using ShopsRU.DiscountAPI.Dtos.Response;
using ShopsRU.DiscountAPI.Model;
using ShopsRU.DiscountAPI.Repositories.Abstract;
using ShopsRU.DiscountAPI.Services.Abstract;
using System.Net;

namespace ShopsRU.DiscountAPI.Services.Concrete
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public ResponseDto<Invoice> GetById(int id)
        {
            return ResponseDto<Invoice>.Success(_invoiceRepository.GetById(id),(int)HttpStatusCode.OK);
        }
    }
}
