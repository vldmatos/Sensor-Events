using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sensors
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
		}

		public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
		{
			if (webHostEnvironment.IsDevelopment())
			{
				applicationBuilder.UseDeveloperExceptionPage();
			}
			else
			{
				applicationBuilder.UseExceptionHandler("/Error");		
				applicationBuilder.UseHsts();
			}

			applicationBuilder.UseHttpsRedirection();
			applicationBuilder.UseStaticFiles();

			applicationBuilder.UseRouting();
			applicationBuilder.UseAuthorization();
			applicationBuilder.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
