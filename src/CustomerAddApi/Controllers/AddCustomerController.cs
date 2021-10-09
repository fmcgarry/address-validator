using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AddressValidation;
using AddressValidation.Web.Models;
using AddressValidation.Web.Converters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAddApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AddCustomerController : ControllerBase
	{
		private readonly UspsAddressValidator addressValidator;
		private readonly ILogger<AddCustomerController> logger;

		public AddCustomerController(ILogger<AddCustomerController> logger, UspsAddressValidator addressValidator)
		{
			this.logger = logger;
			this.addressValidator = addressValidator;
		}

		[HttpPost]
		public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
		{
			if (ModelState.IsValid)
			{
				var coreCustomer = customer.ToCoreCustomer();

				if (coreCustomer is not null)
				{
					await addressValidator.ValidateCustomerAddressAsync(coreCustomer);
					return Ok(coreCustomer);
				}
			}

			return new JsonResult("Something went wrong.") { StatusCode = 500 };
		}
	}
}