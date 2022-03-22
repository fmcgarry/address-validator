using System;
using AddressValidation.Core.Crm;
using AddressValidation.Core.Interfaces;
using AddressValidation.Core.UspsApi;
using AddressValidator.Core.UspsApi.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AddressValidation.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressValidation.Web v1"));
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
			.AddJsonOptions(configure =>
			{
				configure.JsonSerializerOptions.AllowTrailingCommas = true;
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressValidation.Web", Version = "v1" });
			});
			services.AddHttpClient(UspsAddressValidator.HttpClientName, c =>
			{
				c.BaseAddress = new Uri(Configuration.GetConnectionString("UspsEndpoint"));
			});

			services.AddTransient<IAddressValidator, Core.AddressValidator>();
			services.AddTransient<ICrmRepository, CrmRepository>();
			services.AddTransient<ICrmRepository, CrmRepository>();
			services.AddTransient<IUspsAddressValidator, UspsAddressValidator>();
			services.AddSingleton(Configuration);
		}
	}
}