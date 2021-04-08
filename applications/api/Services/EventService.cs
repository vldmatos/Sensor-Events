using API.Models;
using API.Settings;
using App.Metrics;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
	public class EventService
	{
		private const string StatusProcessed = "processed";
		private const string StatusError = "error";
		private const int LimitLast = 20;

		#region Fields

		private readonly IMongoCollection<Event> Events;

		#endregion Fields

		#region Constructors

		public EventService(IDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			Events = database.GetCollection<Event>(settings.EventCollectionName);
		}

		#endregion Constructors

		#region Methods

		public List<Event> Last()
		{
			return
			Events.Find(@event => true)
				  .SortByDescending(@event => @event.Time)
				  .Limit(LimitLast)
				  .ToList();
		}

		public dynamic Values()
		{
			return
			new
			{ 
				errors = Events.CountDocuments(@event => @event.Status == StatusError),
				processed = Events.CountDocuments(@event => @event.Status == StatusProcessed)
			};
		}

		public long Region(string tag)
		{
			return
			Events.CountDocuments(@event => @event.Tag.StartsWith(tag));
		}

		public Event Get(string id) => Events.Find(@event => @event.Id == id).FirstOrDefault();

		public Event Process(Event @event)
		{
			@event.Status = (string.IsNullOrEmpty(@event.Value) || string.IsNullOrWhiteSpace(@event.Value)) ? StatusError : StatusProcessed;

			Events.InsertOne(@event);
			return @event;
		}

		public void Update(string id, Event @eventIn) => Events.ReplaceOne(@event => @event.Id == id, @eventIn);

		public void Remove(Event @eventIn) => Events.DeleteOne(@event => @event.Id == @eventIn.Id);

		public void Remove(string id) => Events.DeleteOne(@event => @event.Id == id); 

		#endregion Methods
	}
}
