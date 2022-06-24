using Moq;
using ShopsRU.DiscountAPI.Common.Enums;
using ShopsRU.DiscountAPI.Dtos.Response;
using ShopsRU.DiscountAPI.Model;
using ShopsRU.DiscountAPI.Repositories.Abstract;
using ShopsRU.DiscountAPI.Services.Abstract;
using ShopsRU.DiscountAPI.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace ShopsRU.DiscountAPI.Test.Services
{
    public class DiscountServiceTest
    {

        private readonly Mock<IDiscountRepository> _discountRepository;
        private readonly Mock<IInvoiceService> _invoiceService;
        private readonly Mock<ICustomerService> _customerService;

        private readonly DiscountService _discountService;

        public DiscountServiceTest()
        {
            _discountRepository = new Mock<IDiscountRepository>();
            _invoiceService = new Mock<IInvoiceService>();
            _customerService = new Mock<ICustomerService>();

            _discountService = new DiscountService(_discountRepository.Object, _invoiceService.Object, _customerService.Object);


        }

        [Fact]
        public void Calculate_NotExistInvoice_ThrowArgumentException()
        {
            _invoiceService.Setup(d => d.GetById(It.IsAny<int>())).Returns(() => null);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => _discountService.Calculate(1));

            Assert.Equal("Invoice not found.", exception.Message);
        }

        [Fact]
        public void Calculate_NotExistCustomer_ThrowArgumentException()
        {
            var invoice = ResponseDto<Invoice>.Success(new Invoice { Id = 1, CustomerId = 1, Amount = 200, Type = Common.Enums.InvoiceTypes.Groceries },(int)HttpStatusCode.OK);

            _invoiceService.Setup(d => d.GetById(It.IsAny<int>())).Returns(() => invoice);
            _customerService.Setup(d => d.GetById(It.IsAny<int>())).Returns(() => null);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => _discountService.Calculate(1));

            Assert.Equal("Customer not found.", exception.Message);
        }

        [Theory]
        [InlineData(InvoiceTypes.NonGroceries, 250, 165)]
        [InlineData(InvoiceTypes.Groceries, 250, 240)]
        public void Calculate_Employee_ReturnDiscountedPrice(InvoiceTypes invoceType, decimal amount, decimal expectedDiscount)
        {
            var invoice = ResponseDto<Invoice>.Success(new Invoice { Id = 1, CustomerId = 1, Amount = amount, Type = invoceType }, (int)HttpStatusCode.OK);
            var customer = ResponseDto<Customer>.Success(new Customer { Id = 1, Name = "testName", CreatedAt = DateTime.Now, Type = CustomerTypes.Employee }, (int)HttpStatusCode.OK);
            
            _invoiceService.Setup(d => d.GetById(It.IsAny<int>())).Returns(() => invoice);
            _customerService.Setup(d => d.GetById(1)).Returns(() => customer);
            _discountRepository.Setup(d => d.GetByTypes(It.IsAny<List<DiscountTypes>>()))
                .Returns(() => Discounts(new List<DiscountTypes> { DiscountTypes.Percent30, DiscountTypes.Discount5ForEvery100 }));

            var result = _discountService.Calculate(1);

            Assert.IsType<ResponseDto<decimal>>(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Errors);
            Assert.Equal(expectedDiscount, result.Data);
        }

        [Theory]
        [InlineData(InvoiceTypes.NonGroceries, 250, 215)]
        [InlineData(InvoiceTypes.Groceries, 250, 240)]
        public void Calculate_Member_ReturnDiscountedPrice(InvoiceTypes invoceType, decimal amount, decimal expectedDiscount)
        {
            var invoice = ResponseDto<Invoice>.Success(new Invoice { Id = 1, CustomerId =1, Amount = amount, Type = invoceType }, (int)HttpStatusCode.OK);
            var customer = ResponseDto<Customer>.Success(new Customer { Id = 1, Name = "testName", CreatedAt = DateTime.Now, Type = CustomerTypes.Member }, (int)HttpStatusCode.OK);

            _invoiceService.Setup(d => d.GetById(It.IsAny<int>())).Returns(() => invoice);
            _customerService.Setup(d => d.GetById(1)).Returns(() => customer);
            _discountRepository.Setup(d => d.GetByTypes(It.IsAny<List<DiscountTypes>>()))
                .Returns(() => Discounts(new List<DiscountTypes> { DiscountTypes.Percen10, DiscountTypes.Discount5ForEvery100 }));

            var result = _discountService.Calculate(1);

            Assert.IsType<ResponseDto<decimal>>(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Errors);
            Assert.Equal(expectedDiscount, result.Data);
        }

        [Theory]
        [InlineData(InvoiceTypes.NonGroceries, 250, 227.5)]
        [InlineData(InvoiceTypes.Groceries, 250, 240)]
        public void Calculate_LognTermCustomer_ReturnDiscountedPrice(InvoiceTypes invoceType, decimal amount, decimal expectedDiscount)
        {
            var invoice = ResponseDto<Invoice>.Success(new Invoice { Id = 1, CustomerId = 1, Amount = amount, Type = invoceType }, (int)HttpStatusCode.OK);
            var customer = ResponseDto<Customer>.Success(new Customer { Id = 1, Name = "testName", CreatedAt = DateTime.Now, Type = CustomerTypes.Customer }, (int)HttpStatusCode.OK);

            _invoiceService.Setup(d => d.GetById(It.IsAny<int>())).Returns(() => invoice);
            _customerService.Setup(d => d.GetById(1)).Returns(() => customer);
            _discountRepository.Setup(d => d.GetByTypes(It.IsAny<List<DiscountTypes>>()))
                .Returns(() => Discounts(new List<DiscountTypes> { DiscountTypes.Percent5, DiscountTypes.Discount5ForEvery100 }));

            var result = _discountService.Calculate(1);

            Assert.IsType<ResponseDto<decimal>>(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Errors);
            Assert.Equal(expectedDiscount, result.Data);
        }

        [Theory]
        [InlineData(InvoiceTypes.NonGroceries, 250, 240)]
        [InlineData(InvoiceTypes.Groceries, 250, 240)]
        public void Calculate_Customer_ReturnDiscountedPrice(InvoiceTypes invoceType, decimal amount, decimal expectedDiscount)
        {
            var invoice = ResponseDto<Invoice>.Success(new Invoice { Id = 1, CustomerId = 1, Amount = amount, Type = invoceType }, (int)HttpStatusCode.OK);
            var customer = ResponseDto<Customer>.Success(new Customer { Id = 1, Name = "testName", CreatedAt = DateTime.Now, Type = CustomerTypes.Customer }, (int)HttpStatusCode.OK);

            _invoiceService.Setup(d => d.GetById(It.IsAny<int>())).Returns(() => invoice);
            _customerService.Setup(d => d.GetById(1)).Returns(() => customer);
            _discountRepository.Setup(d => d.GetByTypes(It.IsAny<List<DiscountTypes>>()))
                .Returns(() => Discounts(new List<DiscountTypes> { DiscountTypes.Discount5ForEvery100 }));

            var result = _discountService.Calculate(1);

            Assert.IsType<ResponseDto<decimal>>(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Errors);
            Assert.Equal(expectedDiscount, result.Data);
        }

        private List<Discount> Discounts(List<DiscountTypes> types)
        {
            var discounts = new List<Discount>()
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

            return discounts.Where(d => types.Contains(d.DiscountType)).ToList();
        }
    }
}