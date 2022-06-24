using ShopsRU.DiscountAPI.Dtos.Response;
using ShopsRU.DiscountAPI.Model;

namespace ShopsRU.DiscountAPI.Services.Abstract
{
    public interface IDiscountService
    {
        ResponseDto<decimal> Calculate(int invoiceId);
    }
}
