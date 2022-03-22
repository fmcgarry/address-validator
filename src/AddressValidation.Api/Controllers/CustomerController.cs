using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AddressValidation.Web.Models;
using AddressValidation.Web.Converters;
using AddressValidation.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AddressValidation.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly IAddressValidator addressValidator;
		private readonly ILogger<CustomerController> logger;

		public CustomerController(ILogger<CustomerController> logger, IAddressValidator addressValidator)
		{
			this.logger = logger;
			this.addressValidator = addressValidator;
		}

		[HttpPost]
		public async Task<ActionResult<CustomerDTO>> AddCustomer([FromBody] CustomerDTO customer)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					// Apparently the ApiController attribute does this automatically,
					// but it won't work with the tests so we'll manually handle it.
					return BadRequest(ModelState);
				}

				var coreCustomer = customer.ToCustomer();

				if (coreCustomer is not null)
				{
					await addressValidator.ValidateAsync(coreCustomer);
					await addressValidator.AddCustomerToCrm(coreCustomer);

					// should actually return 201 status code, but no Get method is implemented.
					// return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
					return Ok(coreCustomer);
				}
				else
				{
					return GenerateUnexpencedErrorResponse($"Failed to convert {nameof(CustomerDTO)} to {nameof(AddressValidation.Core.Models.Customer)}");
				}
			}
			catch (System.Exception e)
			{
				// TODO: Add an actual error page: https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-5.0
				return GenerateUnexpencedErrorResponse(e.ToString());
			}
		}

		private JsonResult GenerateUnexpencedErrorResponse(string logMessage)
		{
			logger.LogError(logMessage);
			return new JsonResult("An unexpected error has occurred.") { StatusCode = 500 };
		}
	}
}