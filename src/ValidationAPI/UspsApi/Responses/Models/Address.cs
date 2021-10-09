using System.Xml.Serialization;

namespace AddressValidation.Core.UspsApi.Responses.Models
{
	[XmlRoot(ElementName = "Address")]
	public class Address
	{
		[XmlElement(ElementName = "Address1")]
		public string Address1 { get; set; } = "";

		[XmlElement(ElementName = "Address2")]
		public string Address2 { get; set; } = "";

		[XmlElement(ElementName = "Address2Abbreviation")]
		public string Address2Abbreviation { get; set; } = "";

		[XmlElement(ElementName = "Business")]
		public string Business { get; set; } = "";

		[XmlElement(ElementName = "CarrierRoute")]
		public string CarrierRoute { get; set; } = "";

		[XmlElement(ElementName = "CentralDeliveryPoint")]
		public string CentralDeliveryPoint { get; set; } = "";

		[XmlElement(ElementName = "City")]
		public string City { get; set; } = "";

		[XmlElement(ElementName = "CityAbbreviation")]
		public string CityAbbreviation { get; set; } = "";

		[XmlElement(ElementName = "DeliveryPoint")]
		public int DeliveryPoint { get; set; }

		[XmlElement(ElementName = "DPVCMRA")]
		public string DPVCMRA { get; set; } = "";

		[XmlElement(ElementName = "DPVConfirmation")]
		public string DPVConfirmation { get; set; } = "";

		public string DPVFootnotes { get; set; } = "";

		[XmlElement(ElementName = "Error")]
		public Error Error { get; set; }

		[XmlElement(ElementName = "FirmName")]
		public string FirmName { get; set; } = "";

		[XmlElement(ElementName = "Footnotes")]
		public string Footnotes { get; set; } = "";

		[XmlElement(ElementName = "ID")]
		public int ID { get; set; }

		public bool IsValid => Error is null;

		[XmlElement(ElementName = "State")]
		public string State { get; set; } = "";

		[XmlElement(ElementName = "Vacant")]
		public string Vacant { get; set; } = "";

		[XmlElement(ElementName = "Zip4")]
		public string Zip4 { get; set; }

		[XmlElement(ElementName = "Zip5")]
		public string Zip5 { get; set; }
	}
}