using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AddressValidation.Core.UspsApi.Requests.Models;

namespace AddressValidation.Core.UspsApi.Requests
{
	[XmlRoot(ElementName = "AddressValidateRequest")]
	public class AddressValidateRequest
	{
		[XmlElement(ElementName = "Address", Order = 2)]
		public Address Address { get; set; }

		[XmlElement(ElementName = "Revision", Order = 1)]
		public int Revision { get; set; }

		[XmlAttribute(AttributeName = "USERID")]
		public string UserId { get; set; }
	}
}