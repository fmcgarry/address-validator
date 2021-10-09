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
		public async Task<ActionResult<CustomerDTO>> AddCustomer([FromBody] CustomerDTO customer)
		{
			var coreCustomer = customer.ToCustomer();

			if (coreCustomer is not null)
			{
				await addressValidator.ValidateCustomerAddressAsync(coreCustomer);

				// should actually return 201 status code, but no Get method is implemented.
				// return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
				return Ok(coreCustomer);
			}
			else
			{
				logger.LogError($"Failed to convert {nameof(CustomerDTO)} to {nameof(AddressValidation.Core.Models.Customer)}");
			}

			return new JsonResult("Something went wrong.") { StatusCode = 500 };
		}
	}
}