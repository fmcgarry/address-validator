using System;
using System.Threading.Tasks;
using AddressValidation.Core.Models;
using Microsoft.Extensions.Logging;

namespace AddressValidator.Core
{
	public class CrmRepository : ICrmRepository
	{
		private readonly ILogger logger;

		public CrmRepository(ILogger<CrmRepository> logger)
		{
			this.logger = logger;
		}

		public Task UpsertCustomer(Customer customer)
		{
			logger.LogInformation($"Upserted customer '{customer.CustomerName}'");

			return Task.CompletedTask;
		}
	}
}