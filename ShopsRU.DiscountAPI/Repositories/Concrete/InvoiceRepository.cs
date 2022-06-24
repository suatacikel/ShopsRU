using ShopsRU.DiscountAPI.Common.MockDatas;
using ShopsRU.DiscountAPI.Model;
using ShopsRU.DiscountAPI.Repositories.Abstract;
using System.Linq;


namespace ShopsRU.DiscountAPI.Repositories.Concrete
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public Invoice GetById(int id)
        {
            return new MockData().Invoices().FirstOrDefault(d => d.Id == id);
        }
    }
}
