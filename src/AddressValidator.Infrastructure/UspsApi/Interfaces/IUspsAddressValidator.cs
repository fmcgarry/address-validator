using System.Threading.Tasks;
using AddressValidation.Core.Models;

namespace AddressValidator.Infrastructure.UspsApi.Interfaces
{
	public interface IUspsAddressValidator
	{
		Task<Customer> ValidateCustomerAddressAsync(Customer customer);
	}
}