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
using AddressValidation.Models.Concrete;
using AddressValidation.Requests;
using AddressValidation.Responses;
using AddressValidation.Core.Models;

namespace AddressValidation
{
	public class UspsAddressValidator
	{
		private readonly HttpClient client;
		private readonly string userId;

		public UspsAddressValidator(string userId = "775SELF04801")
		{
			client = new HttpClient()
			{
				BaseAddress = new Uri("https://secure.shippingapis.com")
			};

			this.userId = userId;
		}

		public T DeserializeFromXmlString<T>(string text)
		{
			var serializer = new XmlSerializer(typeof(T));

			using var writer = new StringReader(text);
			var obj = (T)serializer.Deserialize(writer);

			return obj;
		}

		public string SerializeToXmlString<T>(T objectToSerialize)
		{
			var sb = new StringBuilder();
			using var writer = XmlWriter.Create(sb);

			var serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(writer, objectToSerialize);

			return sb.ToString();
		}

		public async Task<Customer> ValidateCustomerAddressAsync(Customer customer)
		{
			var request = new AddressValidateRequest()
			{
				UserId = userId,
				Revision = 1,
				Address = new Models.Concrete.Address()
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

			//var response2 = await SendRequestAsync(request);
		}

		//public string SerializeToXml<T>(T objectToSerialize)
		//{
		//	var namespaces = new XmlSerializerNamespaces();
		//	namespaces.Add("", "");

		//	var settings = new XmlWriterSettings { OmitXmlDeclaration = true };

		//	var sb = new StringBuilder();
		//	using var writer = XmlWriter.Create(sb, settings);

		//	var serializer = new XmlSerializer(typeof(T));
		//	serializer.Serialize(writer, objectToSerialize, namespaces);

		//	return sb.ToString();
		//}
		/// <summary>
		/// Send a REST call to the provided destination.
		/// </summary>
		/// <typeparam name="T">The response type.</typeparam>
		/// <param name="destination">The url of the REST call.</param>
		/// <returns>Task with result type T.</returns>
		private async Task<U> SendRequestAsync<T, U>(T request)
		{
			try
			{
				//var asdf = System.Net.WebUtility.UrlEncode(destination);
				//var response = await client.GetAsync(destination);

				HttpResponseMessage response = await client.PostAsXmlAsync(new Uri("https://secure.shippingapis.com?API=Verify&XML="), request);
				response.EnsureSuccessStatusCode();

				var afds = await response.Content.ReadAsStringAsync();

				U quoteResponse = await response.Content.ReadAsAsync<U>();
				return quoteResponse;
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		///// <summary>
		///// Send a REST call to the provided destination.
		///// </summary>
		///// <typeparam name="T">The response type.</typeparam>
		///// <param name="destination">The url of the REST call.</param>
		///// <returns>Task with result type T.</returns>
		//private async Task<string> SendRequestAsync<T>(T destination)
		//{
		//	try
		//	{
		//		HttpResponseMessage response = await client.PostAsXmlAsync(client.BaseAddress + "?API=Verify&XML=", destination);

		//		response.RequestMessage
		//		response.EnsureSuccessStatusCode();

		//		var quoteResponse = await response.Content.ReadAsStringAsync();
		//		return quoteResponse;
		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e);
		//		throw;
		//	}
		//}

		/// <summary>
		/// Send a REST call to the provided destination.
		/// </summary>
		/// <typeparam name="T">The response type.</typeparam>
		/// <param name="destination">The url of the REST call.</param>
		/// <returns>Task with result type T.</returns>
		private async Task<string> SendRequestAsync(string destination)
		{
			try
			{
				HttpResponseMessage response = await client.GetAsync($"?API=Verify&XML={WebUtility.UrlEncode(destination)}");

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