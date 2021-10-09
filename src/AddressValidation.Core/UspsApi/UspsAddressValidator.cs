using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using AddressValidation.Core.Models;
using Microsoft.Extensions.Configuration;
using AddressValidator.Core.UspsApi.Interfaces;
using AddressValidation.Core.UspsApi.Responses;
using AddressValidation.Core.UspsApi.Requests;

namespace AddressValidation.Core.UspsApi
{
	public class UspsAddressValidator : IUspsAddressValidator
	{
		private static readonly HttpClient client = new();
		private readonly IConfiguration config;
		private readonly string userId;

		public UspsAddressValidator(IConfiguration config)
		{
			this.config = config;
			userId = this.config.GetConnectionString("UspsUserId");
		}

		public async Task<Customer> ValidateCustomerAddressAsync(Customer customer)
		{
			var request = new AddressValidateRequest()
			{
				UserId = userId,
				Revision = 1,
				Address = new Requests.Models.Address()
				{
					Id = 0,
					Address1 = customer.Address.Line1,
					City = customer.Address.City,
					State = customer.Address.State,
					Zip5 = customer.Address.PostalCode,
				}
			};

			string rawRequest = SerializeToXmlString(request);

			string rawResponse = await SendRequestAsync(rawRequest);

			AddressValidateResponse response = DeserializeFromXmlString<AddressValidateResponse>(rawResponse);

			if (response.Address.IsValid)
			{
				customer.Address.City = response.Address.City;
				customer.Address.Line1 = response.Address.Address2;
				customer.Address.PostalCode = $"{response.Address.Zip5}-{response.Address.Zip4}";
				customer.Address.State = response.Address.State;
			}

			return customer;
		}

		private static T DeserializeFromXmlString<T>(string text)
		{
			var serializer = new XmlSerializer(typeof(T));

			using var writer = new StringReader(text);
			var obj = (T)serializer.Deserialize(writer);

			return obj;
		}

		private static string SerializeToXmlString<T>(T objectToSerialize)
		{
			var sb = new StringBuilder();
			using var writer = XmlWriter.Create(sb);

			var serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(writer, objectToSerialize);

			return sb.ToString();
		}

		private async Task<string> SendRequestAsync(string destination)
		{
			try
			{
				HttpResponseMessage response = await client.GetAsync(config.GetConnectionString("UspsEndpoint") + WebUtility.UrlEncode(destination));

				var quoteResponse = await response.Content.ReadAsStringAsync();
				return quoteResponse;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}