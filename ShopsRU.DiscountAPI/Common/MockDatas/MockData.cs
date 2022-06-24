using ShopsRU.DiscountAPI.Common.Enums;
using ShopsRU.DiscountAPI.Model;

namespace ShopsRU.DiscountAPI.Common.MockDatas
{
    public class MockData
    {
        public List<Discount> Discounts()
        {
            return new List<Discount>()
            {
                 new Discount
                  {
                      Id = 1,
                      DiscountType = DiscountTypes.Percent30,
                      DiscountCalculateType = DiscountCalculateTypes.Percent,
                      Amount = 100,
                      Rate = 30,
                      AllowedInvoiceTypes = new List<InvoiceTypes>{InvoiceTypes.NonGroceries}
                  },
                  new Discount
                  {
                      Id = 2,
                      DiscountType = DiscountTypes.Percen10,
                      DiscountCalculateType = DiscountCalculateTypes.Percent,
                      Amount = 100,
                      Rate = 10,
                      AllowedInvoiceTypes = new List<InvoiceTypes>{InvoiceTypes.NonGroceries}
                  },
                  new Discount
                  {
                      Id = 3,
                      DiscountType = DiscountTypes.Percent5,
                      DiscountCalculateType = DiscountCalculateTypes.Percent,
                      Amount = 100,
                      Rate = 5,
                      AllowedInvoiceTypes = new List<InvoiceTypes>{InvoiceTypes.NonGroceries}
                  },
                  new Discount
                  {
                      Id = 4,
                      DiscountType = DiscountTypes.Discount5ForEvery100,
                      DiscountCalculateType = DiscountCalculateTypes.PerAmount,
                      Amount = 100,
                      Rate = 5,
                      AllowedInvoiceTypes = new List<InvoiceTypes>{InvoiceTypes.NonGroceries,InvoiceTypes.Groceries}
                  },
            };
        }

        public List<Customer> Customers()
        {
            return new List<Customer>()
            {
                 new Customer
                  {
                      Id = 1,
                      CreatedAt = DateTime.Now,
                      Name = "Cust-Employee",
                      Type = CustomerTypes.Employee
                  },
                 new Customer
                  {
                      Id = 2,
                      CreatedAt = DateTime.Now,
                      Name = "Cust-Member",
                      Type = CustomerTypes.Member
                  },
                 new Customer
                  {
                      Id = 3,
                      CreatedAt = DateTime.Now.AddYears(-1).AddDays(-1),
                      Name = "Cust-LongTermCustomer",
                      Type = CustomerTypes.Customer
                  },
                 new Customer
                  {
                      Id = 4,
                      CreatedAt = DateTime.Now,
                      Name = "Cust-Employee",
                      Type = CustomerTypes.Customer
                  },
            };
        }

        public List<Invoice> Invoices()
        {
            return new List<Invoice>()
            {
                 new Invoice
                 {
                      Id = 1,
                      Amount = 100,
                      CustomerId = 1,
                      Type = InvoiceTypes.NonGroceries
                 },
                 new Invoice
                 {
                      Id = 2,
                      Amount = 200,
                      CustomerId = 2,
                      Type = InvoiceTypes.Groceries
                 },
                 new Invoice
                 {
                      Id = 3,
                      Amount = 990,
                      CustomerId = 2,
                      Type = InvoiceTypes.Groceries
                 }
            };
        }
    }
}
