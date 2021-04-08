using App.Metrics;
using App.Metrics.Counter;

namespace API.Metrics
{
	public class MetricsRegistry
	{
		public static CounterOptions EventCreateCounter => new CounterOptions
		{
			Name = "Call to create event",
			Context = Startup.Name,
			MeasurementUnit = Unit.Calls
		};

		public static CounterOptions EventLastCounter => new CounterOptions
		{
			Name = "Call Last events created",
			Context = Startup.Name,
			MeasurementUnit = Unit.Calls
		};

		public static CounterOptions EventValuesCounter => new CounterOptions
		{
			Name = "Call valeus events created",
			Context = Startup.Name,
			MeasurementUnit = Unit.Calls
		};

		public static CounterOptions EventRegionCounter => new CounterOptions
		{
			Name = "Call region events created",
			Context = Startup.Name,
			MeasurementUnit = Unit.Calls
		};
	}
}