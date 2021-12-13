namespace AddressValidation.Web
{
	public static class CustomConfigurationExtensions
	{
		public static string GetHomePageFeature(this IConfiguration config, string name)
		{
			return config?.GetSection("Features:HomePage")?[name];
		}
	}
}
