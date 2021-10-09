using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressValidation.Web.Converters
{
	public static class ModelConverters
	{
		public static Core.Models.Address? ToCoreAddress(this Models.Address address)
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
			catch (Exception e)
			{
				return null;
			}
		}

		public static Core.Models.Customer? ToCoreCustomer(this Models.Customer customer)
		{
			try
			{
				return new Core.Models.Customer()
				{
					CustomerName = customer.CustomerName,
					CustomerEmail = customer.CustomerEmail,
					Address = customer.Address.ToCoreAddress()
				};
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}