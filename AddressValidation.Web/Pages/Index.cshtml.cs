using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressValidation.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IConfiguration _config;

		public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}
		public bool ShowGreeting { get; set; }

		public void OnGet()
		{
			var features = _config.GetSection("Features:IndexPage");

			if (features.GetValue<bool>("ShowGreeting"))
			{
				ShowGreeting = true;
				_logger.LogInformation("Displaying Greeting");
			}
		}
	}
}