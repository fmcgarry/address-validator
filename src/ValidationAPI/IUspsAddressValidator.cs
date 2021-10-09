using System.Threading.Tasks;
using AddressValidation.Core.Models;

namespace AddressValidation
{
	public interface IUspsAddressValidator
	{
		Task<Customer> ValidateCustomerAddressAsync(Customer customer);
	}
}