using ShopsRU.DiscountAPI.Dtos.Response;
using ShopsRU.DiscountAPI.Model;

namespace ShopsRU.DiscountAPI.Services.Abstract
{
    public interface IInvoiceService
    {
        ResponseDto<Invoice> GetById(int id);
    }
}
