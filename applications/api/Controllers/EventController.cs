using API.Metrics;
using API.Security;
using API.Services;
using App.Metrics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly EventService _service;
		private readonly ILogger<EventController> _logger;
		private readonly IMetrics _metrics;

		public EventController(EventService service, ILogger<EventController> logger, IMetrics metrics)
		{
			_service = service;
			_logger = logger;
			_metrics = metrics;
		}

		[HttpPost]
		[Route("receive")]
		[Authorize(Roles = Roles.Creator)]
		public ActionResult<Models.Event> Receive(Models.Event @event)
		{
			try
			{
				if (@event is null)
					return BadRequest(new { message = $"Invalid parameter {nameof(@event)}" });

				@event.Creator = User.Identity.Name;

				_service.Process(@event);
				
				_logger.LogInformation("Processed", @event);
				_metrics.Measure.Counter.Increment(MetricsRegistry.EventCreateCounter);

				return Ok(CreatedAtAction(nameof(Receive), @event));
			}
			catch (Exception exception)
			{
				var message = "Process Error";

				_logger.LogError(exception, message, @event);

				return StatusCode(StatusCodes.Status500InternalServerError, new { message });
			}
		}

		[HttpGet]
		[Route("latest")]
		//[Authorize(Roles = Roles.Viewer)]
		public ActionResult<IEnumerable<Models.Event>> Latest()
		{
			try
			{
				_metrics.Measure.Counter.Increment(MetricsRegistry.EventLastCounter);

				return Ok(_service.Last());
			}
			catch (Exception exception)
			{
				var message = "Get last error";

				_logger.LogError(exception, message);

				return StatusCode(StatusCodes.Status500InternalServerError, new { message });
			}
		}

		[HttpGet]
		[Route("values")]
		[Authorize(Roles = Roles.Viewer)]
		public ActionResult<dynamic> Values()
		{
			try
			{
				_metrics.Measure.Counter.Increment(MetricsRegistry.EventValuesCounter);

				return Ok(_service.Values());
			}
			catch (Exception exception)
			{
				var message = "Get total with values error";

				_logger.LogError(exception, message);

				return StatusCode(StatusCodes.Status500InternalServerError, new { message });
			}
		}

		[HttpGet]
		[Route("region")]
		[Authorize(Roles = Roles.Viewer)]

		public ActionResult<long> Region(string tag)
		{
			try
			{
				if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
					return BadRequest(new { message = $"Invalid parameter {nameof(tag)}" });

				_metrics.Measure.Counter.Increment(MetricsRegistry.EventRegionCounter);

				return Ok(_service.Region(tag.ToLower()));
			}
			catch (Exception exception)
			{
				var message = "Get total with values error";

				_logger.LogError(exception, message);

				return StatusCode(StatusCodes.Status500InternalServerError, new { message });
			}
		}
	}
}
