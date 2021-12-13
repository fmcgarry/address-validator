using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AddressValidation.Web
{
	public static class CustomServiceCollectionExtensions
	{
		public static IServiceCollection AddSomethingCustom(this IServiceCollection services, IConfiguration config)
		{
			if (config.GetValue<bool>("Features:IndexPage:ShowGreeting"))
			{
				//services.TryAddSingleton<ISomeType, SomeType>();
			}
			else
			{
				//services.TryAddSingleton<ISomeType, SomeOtherType>();
			}

			return services;
		}
	}
}
