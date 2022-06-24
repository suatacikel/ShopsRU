using ShopsRU.DiscountAPI.Common.Enums;

namespace ShopsRU.DiscountAPI.Model
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerTypes Type { get; set; }
    }
}
