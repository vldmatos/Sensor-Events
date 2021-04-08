using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sensors.Models
{
	public class Sensor
	{
		#region Properties

		public int Limit { get; init; }

		public string Name { get; init; }

		public string Region { get; init; }

		public string Instance => Guid.NewGuid().ToString();

		private static Random Random => new Random();

		#endregion Properties

		#region Constructors
		
		public Sensor(string name, string region, int limit) =>
			(Name, Region, Limit) = (name, region, limit);

		#endregion Constructors

		#region Methods

		public async IAsyncEnumerable<Event> Trigger(int count)
		{
			for (int i = 0; i < count; i++)
			{
				yield return
				await Task.Run(() => 
				{
					return
					new Event()
					{
						Sensor = Name.ToLower(),
						Time = DateTimeOffset.Now.ToUnixTimeMilliseconds(),						
						Tag = Region.ToLower(),
						Value = GenerateValue()
					};
				});
			}
		}

		private string GenerateValue()
		{
			var value = Random.Next((int)Math.Ceiling(Limit * 2.0));
			return (value > Limit) ? string.Empty : value.ToString();
		} 

		#endregion Methods
	}
}
