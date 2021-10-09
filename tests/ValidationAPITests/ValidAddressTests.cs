using System.Threading.Tasks;
using AddressValidation;
using AddressValidation.Core.Models;
using AddressValidation.Web.Converters;
using AddressValidation.Web.Models;
using AddressValidator.Infrastructure;
using CustomerAddApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ValidationAPITests
{
	[TestClass]
	public class ValidAddressTests
	{
		[TestMethod]
		public async Task Post_ModelStateInvalid_ReturnsBadRequestResult()
		{
			var customer = new CustomerDTO();
			var mockLogger = new Mock<ILogger<CustomerController>>();
			var mockRepo = new Mock<ICrmRepository>();
			var mockUspsApi = new Mock<IUspsAddressValidator>();
			mockUspsApi.Setup(x => x.ValidateCustomerAddressAsync(It.IsAny<Customer>())).Returns(Task.FromResult(customer.ToCustomer()));

			var controller = new CustomerController(mockLogger.Object, mockUspsApi.Object, mockRepo.Object);
			controller.ModelState.AddModelError("CustomerName", "Required");

			ActionResult<CustomerDTO> result = await controller.AddCustomer(customer);

			var contentResult = result.Result as BadRequestObjectResult;
			Assert.IsNotNull(contentResult);
		}

		[TestMethod]
		public async Task Post_ValidModelState_ReturnsOkObjectResult()
		{
			var customer = new CustomerDTO()
			{
				CustomerName = "test",
				CustomerEmail = "test@test.com",
				Address = new AddressDTO()
				{
					City = "",
					Country = "US",
					Line1 = "29851 Aventura",
					PostalCode = "92688",
					State = "CA"
				}
			};

			var mockLogger = new Mock<ILogger<CustomerController>>();
			var mockRepo = new Mock<ICrmRepository>();
			var mockUspsApi = new Mock<IUspsAddressValidator>();
			mockUspsApi.Setup(x => x.ValidateCustomerAddressAsync(It.IsAny<Customer>())).Returns(Task.FromResult(customer.ToCustomer()));

			var controller = new CustomerController(mockLogger.Object, mockUspsApi.Object, mockRepo.Object);

			ActionResult<CustomerDTO> result = await controller.AddCustomer(customer);

			var contentResult = result.Result as OkObjectResult;
			Assert.IsNotNull(contentResult);
		}
	}
}