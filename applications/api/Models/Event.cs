using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
	public class Event
	{
		#region Properties

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public long Time { get; init; }

		public string Tag { get; init; }

		public string Sensor { get; init; }

		public string Value { get; init; }

		public string Creator { get; set; }

		public string Status { get; set; }

		#endregion Properties
	}
}
