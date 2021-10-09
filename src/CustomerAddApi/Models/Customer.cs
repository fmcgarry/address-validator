using System.ComponentModel.DataAnnotations;

namespace AddressValidation.Web.Models
{
	public class Customer
	{
		public Address Address { get; set; }

		[Required]
		public string CustomerEmail { get; set; }

		[Required]
		public string CustomerName { get; set; }
	}
}