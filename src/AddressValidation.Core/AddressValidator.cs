using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressValidation.Core.Interfaces;
using AddressValidation.Core.Models;
using AddressValidator.Core;
using AddressValidator.Core.UspsApi.Interfaces;

namespace AddressValidation.Core
{
	public class AddressValidator : IAddressValidator
	{
		private readonly IUspsAddressValidator addressValidator;
		private readonly ICrmRepository crm;

		public AddressValidator(IUspsAddressValidator addressValidator, ICrmRepository crm)
		{
			this.addressValidator = addressValidator;
			this.crm = crm;
		}

		public async Task AddCustomerToCrm(Customer customer)
		{
			await addressValidator.ValidateCustomerAddressAsync(customer);
		}

		public async Task ValidateAsync(Customer customer)
		{
			await crm.UpsertCustomer(customer);
		}
	}
}