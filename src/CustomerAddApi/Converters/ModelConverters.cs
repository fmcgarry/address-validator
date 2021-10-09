using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressValidation.Web.Converters
{
	public static class ModelConverters
	{
		public static Core.Models.Address? ToAddress(this Models.AddressDTO address)
		{
			try
			{
				return new Core.Models.Address()
				{
					City = address.City,
					Country = address.Country,
					Line1 = address.Line1,
					PostalCode = address.PostalCode,
					State = address.State
				};
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static Core.Models.Customer? ToCustomer(this Models.CustomerDTO customer)
		{
			try
			{
				return new Core.Models.Customer()
				{
					CustomerName = customer.CustomerName,
					CustomerEmail = customer.CustomerEmail,
					Address = customer.Address.ToAddress()
				};
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}