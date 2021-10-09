using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressValidation.Core.Models;

namespace AddressValidator.Infrastructure
{
	internal interface ICrmRepository
	{
		Task UpsertCustomer(Customer customer);
	}
}