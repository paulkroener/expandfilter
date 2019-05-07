using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpandFilter.Models
{
	public class Person
	{
		[Key]
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Trip> Trips { get; set; }
	}
	public class PersonRest
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public List<TripRest> Trips { get; set; }
	}
}