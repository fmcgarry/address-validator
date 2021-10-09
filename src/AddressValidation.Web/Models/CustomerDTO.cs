using System.ComponentModel.DataAnnotations;

namespace AddressValidation.Web.Models
{
	public class CustomerDTO
	{
		public AddressDTO Address { get; set; }

		[Required]
		public string CustomerEmail { get; set; }

		[Required]
		public string CustomerName { get; set; }
	}
}