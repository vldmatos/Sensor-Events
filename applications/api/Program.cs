using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseMetricsWebTracking()
				.UseMetricsEndpoints(options => 
				{					
					options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
					options.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
					options.EnvironmentInfoEndpointEnabled = false;
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
