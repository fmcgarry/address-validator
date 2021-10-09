using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AddressValidator.Infrastructure.UspsApi.Responses.Models;

namespace AddressValidator.Infrastructure.UspsApi.Responses
{
	[XmlRoot(ElementName = "AddressValidateResponse")]
	public class AddressValidateResponse
	{
		[XmlElement(ElementName = "Address")]
		public Address Address { get; set; }
	}
}