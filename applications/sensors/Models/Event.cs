namespace Sensors.Models
{
	public class Event
	{
		#region Properties

		public long Time { get; init; }

		public string Sensor { get; init; }

		public string Tag { get; init; }

		public string Value { get; init; } 

		#endregion Properties
	}
}
