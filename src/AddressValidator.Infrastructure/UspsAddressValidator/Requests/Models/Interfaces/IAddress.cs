namespace AddressValidation.Models.Interfaces
{
	public interface IAddress
	{
		string Address1 { get; set; }
		string Address2 { get; set; }
		string City { get; set; }
		string FirmName { get; set; }
		int Id { get; set; }
		string State { get; set; }
		string Urbanization { get; set; }
		string Zip4 { get; set; }
		string Zip5 { get; set; }
	}
}