namespace AddressValidation.Responses.Models.Interfaces
{
	public interface IAddress
	{
		string Address1 { get; set; }
		string Address2 { get; set; }
		string Business { get; set; }
		string CarrierRoute { get; set; }
		string CentralDeliveryPoint { get; set; }
		string City { get; set; }
		string CityAbbreviation { get; set; }
		int DeliveryPoint { get; set; }
		string DPVCMRA { get; set; }
		string DPVConfirmation { get; set; }
		string DPVFootnotes { get; set; }
		string Footnotes { get; set; }
		int ID { get; set; }
		string State { get; set; }
		string Vacant { get; set; }
		string Zip4 { get; set; }
		string Zip5 { get; set; }
	}
}