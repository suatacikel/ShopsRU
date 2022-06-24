using ShopsRU.DiscountAPI.Common.Enums;
using ShopsRU.DiscountAPI.Common.MockDatas;
using ShopsRU.DiscountAPI.Model;
using ShopsRU.DiscountAPI.Repositories.Abstract;


namespace ShopsRU.DiscountAPI.Repositories.Concrete
{
    public class DiscountRepository : IDiscountRepository
    {
        public List<Discount> Get()
        {
            var data = new MockData();
            return data.Discounts();
        }

        public List<Discount> GetByTypes(List<DiscountTypes> types)
        {
            var data = new MockData();
            return data.Discounts().Where(d => types.Contains(d.DiscountType)).ToList();
        }
    }
}
