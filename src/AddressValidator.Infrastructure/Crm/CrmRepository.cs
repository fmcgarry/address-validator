using System;
using System.Threading.Tasks;
using AddressValidation.Core.Models;

namespace AddressValidator.Infrastructure
{
	public class CrmRepository : ICrmRepository
	{
		public Task UpsertCustomer(Customer customer)
		{
			throw new NotImplementedException();
		}
	}
}