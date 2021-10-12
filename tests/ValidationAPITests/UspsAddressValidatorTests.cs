using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AddressValidation.Core.Models;
using AddressValidation.Core.UspsApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AddressValidation.Tests
{
	[TestClass]
	public class UspsAddressValidatorTests
	{
		private static readonly HttpClient client = new();

		[TestMethod]
		public async Task InvalidEndpointConnectionString_ThrowsInvalidOperationException1()
		{
			var mockedLogger = new Mock<ILogger<UspsAddressValidator>>();

			var mockedConfigSection = new Mock<IConfigurationSection>();
			mockedConfigSection.SetupGet(m => m[It.Is<string>(s => s == "UspsEndpoint")]).Returns("ddd");

			var mockedConfig = new Mock<IConfiguration>();
			mockedConfig.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockedConfigSection.Object);

			var mockedHttpClientFactory = new Mock<IHttpClientFactory>();
			mockedHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

			var asdf = new UspsAddressValidator(mockedLogger.Object, mockedConfig.Object, mockedHttpClientFactory.Object);

			var customer = new Customer()
			{
				CustomerName = "test",
				CustomerEmail = "test@test.com",
				Address = new Address()
				{
					City = "",
					Country = "US",
					Line1 = "29851 Aventura",
					PostalCode = "92688",
					State = "CA"
				}
			};

			await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => asdf.ValidateCustomerAddressAsync(customer));
		}

		[TestMethod]
		public async Task InvalidUserId_ThrowsInvalidOperationException()
		{
			var mockedLogger = new Mock<ILogger<UspsAddressValidator>>();

			var mockedConfigSection = new Mock<IConfigurationSection>();
			mockedConfigSection.SetupGet(m => m[It.Is<string>(s => s == "UspsUserId")]).Returns("asdfasdf");
			mockedConfigSection.SetupGet(m => m[It.Is<string>(s => s == "UspsEndpoint")]).Returns("https://secure.shippingapis.com?API=Verify&XML=");

			var mockedConfig = new Mock<IConfiguration>();
			mockedConfig.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockedConfigSection.Object);

			var mockedHttpClientFactory = new Mock<IHttpClientFactory>();
			mockedHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

			var asdf = new UspsAddressValidator(mockedLogger.Object, mockedConfig.Object, mockedHttpClientFactory.Object);

			var customer = new Customer()
			{
				CustomerName = "test",
				CustomerEmail = "test@test.com",
				Address = new Address()
				{
					City = "",
					Country = "US",
					Line1 = "29851 Aventura",
					PostalCode = "92688",
					State = "CA"
				}
			};

			await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => asdf.ValidateCustomerAddressAsync(customer));
		}
	}
}