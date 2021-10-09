using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AddressValidation.Core.Responses.Models.Concrete
{
	[XmlRoot(ElementName = "Error")]
	public class Error
	{
		[XmlElement(ElementName = "Description")]
		public string Description { get; set; }

		[XmlElement(ElementName = "HelpContext")]
		public object HelpContext { get; set; }

		[XmlElement(ElementName = "HelpFile")]
		public object HelpFile { get; set; }

		[XmlElement(ElementName = "Number")]
		public int Number { get; set; }

		[XmlElement(ElementName = "Source")]
		public string Source { get; set; }
	}
}