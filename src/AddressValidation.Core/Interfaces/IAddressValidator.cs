using System.Threading.Tasks;
using AddressValidation.Core.Models;

namespace AddressValidation.Core.Interfaces
{
	public interface IAddressValidator
	{
		Task AddCustomerToCrm(Customer customer);

		Task ValidateAsync(Customer customer);
	}
}