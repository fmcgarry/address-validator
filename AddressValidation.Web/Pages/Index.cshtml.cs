using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AddressValidation.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		//private readonly IConfiguration _config;
		private readonly HomePageOptions _options;

		//public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
		//{
		//	_logger = logger;
		//	_config = config;
		//}

		public IndexModel(ILogger<IndexModel> logger, IOptions<HomePageOptions> options)
		{
			_options = options.Value;
			_logger = logger;
		}

		public bool ShowGreeting { get; set; }

		public void OnGet()
		{
			//var features = _config.GetSection("Features:IndexPage");

			//if (features.GetValue<bool>("ShowGreeting"))
			//{
			//	ShowGreeting = true;
			//	_logger.LogInformation("Displaying Greeting");
			//}


			if (_options.EnableGreeting)
			{
				ShowGreeting = true;
				_logger.LogInformation("Displaying Greeting");
			}
		}
	}
}