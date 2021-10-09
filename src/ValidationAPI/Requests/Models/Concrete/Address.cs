using System.Xml.Serialization;
using AddressValidation.Models.Interfaces;

namespace AddressValidation.Models.Concrete
{
	[XmlRoot(ElementName = "Address")]
	public class Address : IAddress
	{
		[XmlElement(ElementName = "Address1", Order = 2)]
		public string Address1 { get; set; } = "";

		[XmlElement(ElementName = "Address2", Order = 3)]
		public string Address2 { get; set; } = "";

		[XmlElement(ElementName = "City", Order = 4)]
		public string City { get; set; } = "";

		[XmlElement(ElementName = "FirmName", Order = 1)]
		public string FirmName { get; set; } = "";

		[XmlAttribute(AttributeName = "ID")]
		public int Id { get; set; }

		[XmlElement(ElementName = "State", Order = 5)]
		public string State { get; set; } = "";

		[XmlElement(ElementName = "Urbanization", Order = 6)]
		public string Urbanization { get; set; } = "";

		[XmlElement(ElementName = "Zip4", Order = 8)]
		public string Zip4 { get; set; } = "";

		[XmlElement(ElementName = "Zip5", Order = 7)]
		public string Zip5 { get; set; } = "";
	}
}