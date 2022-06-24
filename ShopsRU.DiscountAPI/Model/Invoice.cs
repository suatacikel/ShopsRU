using ShopsRU.DiscountAPI.Common.Enums;

namespace ShopsRU.DiscountAPI.Model
{
    public class Invoice : BaseModel
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public InvoiceTypes Type { get; set; }
    }
}
