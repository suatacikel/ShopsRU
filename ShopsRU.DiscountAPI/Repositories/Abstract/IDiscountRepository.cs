using ShopsRU.DiscountAPI.Common.Enums;
using ShopsRU.DiscountAPI.Model;

namespace ShopsRU.DiscountAPI.Repositories.Abstract
{
    public interface IDiscountRepository
    {
        List<Discount> Get();
        List<Discount> GetByTypes(List<DiscountTypes> types);
    }
}
