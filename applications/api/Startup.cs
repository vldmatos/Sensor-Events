using API.Security;
using API.Services;
using API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;

namespace API
{
	public class Startup
	{
		internal const string Name = "Events Sensors API";
		private const string DefaultVersion = "1.0.0";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
			services.AddMetrics();

			services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));

			services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);			
			services.AddSingleton<EventService>();			

			services.AddControllers();
			services.AddAuthentication(options =>
					{
						options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
						options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					})
					.AddJwtBearer(options =>
					{
						options.RequireHttpsMetadata = false;
						options.SaveToken = true;
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Key.TokenPrivate),
							ValidateIssuer = false,
							ValidateAudience = false
						};
					});

			services.AddSwaggerGen(swagger =>
			{
				swagger.SwaggerDoc(DefaultVersion, new OpenApiInfo { Title = Name, Version = DefaultVersion });
			});
		}
		
		public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				application.UseDeveloperExceptionPage();
			}

			application.UseMetricServer("/metrics");
			application.UseHttpMetrics();

			application.UseRouting();

			application.UseCors(options => options
								.AllowAnyOrigin()
								.AllowAnyMethod()
								.AllowAnyHeader());

			application.UseAuthentication();
			application.UseAuthorization();

			application.UseSwagger();
			application.UseSwaggerUI(swagger =>
			{
				swagger.SwaggerEndpoint($"/swagger/{DefaultVersion}/swagger.json", Name);
				swagger.RoutePrefix = "swagger";
			});

			application.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}		
	}
}
