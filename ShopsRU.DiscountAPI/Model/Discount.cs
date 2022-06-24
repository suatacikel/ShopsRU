using ShopsRU.DiscountAPI.Common.Enums;

namespace ShopsRU.DiscountAPI.Model
{
    public class Discount : BaseModel
    {
        public DiscountTypes DiscountType { get; set; }
        public DiscountCalculateTypes DiscountCalculateType { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public List<InvoiceTypes>? AllowedInvoiceTypes { get; set; }

        public bool isValidInvoiceType(InvoiceTypes invoiceType)
        {
            if (AllowedInvoiceTypes == null) return true;

            return AllowedInvoiceTypes.Contains(invoiceType);
        }

        public decimal CalcDiscountPrice(decimal invoiceAmount)
        {
            if (DiscountCalculateType == DiscountCalculateTypes.Percent)
                return invoiceAmount / Amount * Rate;

            if (DiscountCalculateType == DiscountCalculateTypes.PerAmount)
                return (int)(invoiceAmount / Amount) * Rate;

            return 0;
        }
    }
}
