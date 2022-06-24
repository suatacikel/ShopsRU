using ShopsRU.DiscountAPI.Model;

namespace ShopsRU.DiscountAPI.Repositories.Abstract
{
    public interface IInvoiceRepository
    {
        Invoice GetById(int id);
    }
}
