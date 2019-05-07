using System.ComponentModel.DataAnnotations;

namespace ExpandFilter.Models
{
	public class Trip
	{
		[Key]
		public string Id { get; set; }
		public string Name { get; set; }
	}
	public class TripRest
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}