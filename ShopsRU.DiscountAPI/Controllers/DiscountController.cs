using Microsoft.AspNetCore.Mvc;
using ShopsRU.DiscountAPI.Services.Abstract;

namespace ShopsRU.DiscountAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        [Route("calculate/{invoiceId}")]
        public ActionResult Calculate(int invoiceId)
        {
            var result = _discountService.Calculate(invoiceId);

            return Ok(result);
        }

    }
}