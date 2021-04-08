using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sensors.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Sensors.Pages
{
	public class IndexModel : PageModel
	{
		private static new User User => new User() { Username = "user-creator", Password = "092813" };

		[BindProperty]
		public string Name { get; set; }

		[BindProperty]
		public int Limit { get; set; }

		[BindProperty]
		public int CountEvents { get; set; }

		[BindProperty]
		public string Region { get; set; }

		private readonly HttpClient _httpClient;
		private readonly ILogger<IndexModel> _logger;
		private readonly IConfiguration _configuration;

		public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;

			_httpClient = new HttpClient
			{
				BaseAddress = new Uri(_configuration["API-Address"])
			};
		}

		public async void OnPost()
		{
			try
			{
				var identity = await Autenticate(User);				
				if (identity?.Token != null)
				{
					string api = "event/receive";
					var sensor = new Sensor(Name, Region, Limit);
					
					_httpClient.DefaultRequestHeaders.Clear();
					_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", identity.Token);

					await foreach (var @event in sensor.Trigger(CountEvents))
					{
						await _httpClient.PostAsJsonAsync(api, @event);
					}
				}
			}
			catch (Exception exception)
			{
				_logger.Log(LogLevel.Error, 
							$"Event: {nameof(OnPost)} - {exception.Message}", 
							exception);
			}
		}

		private async Task<User> Autenticate(User user)
		{
			try
			{
				string api = "account/login";				
				var response = await _httpClient.PostAsJsonAsync(api, user);

				if (response.IsSuccessStatusCode)
					return await response.Content.ReadFromJsonAsync<User>();
				
			}
			catch (Exception exception)
			{
				_logger.Log(LogLevel.Error,
							$"Event: {nameof(OnPost)} - {exception.Message}",
							exception);
			}

			return default;
		}
	}
}
