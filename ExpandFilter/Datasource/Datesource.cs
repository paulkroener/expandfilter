using System.Collections.Generic;
using ExpandFilter.Models;

namespace ExpandFilter.Datasource
{
	public class DataSource
	{
		private static DataSource instance = null;
		public static DataSource Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DataSource();
				}
				return instance;
			}
		}
		public List<Person> People { get; set; }
		public List<Trip> Trips { get; set; }
		private DataSource()
		{
			Reset();
			Initialize();
		}
		public void Reset()
		{
			People = new List<Person>();
			Trips = new List<Trip>();
		}
		public void Initialize()
		{
			this.Trips.AddRange(new List<Trip>()
			{
				new Trip()
				{
					Id = "0",
					Name = "Trip 0"
				},
				new Trip()
				{
					Id = "1",
					Name = "Trip 1"
				},
				new Trip()
				{
					Id = "2",
					Name = "Trip 2"
				},
				new Trip()
				{
					Id = "3",
					Name = "Trip 3"
				}
			});
			this.People.AddRange(new List<Person>
			{
				new Person()
				{
					Id = "001",
					Name = "Angel",
					Trips = new List<Trip>{Trips[0], Trips[1]}
				},
				new Person()
				{
					Id = "002",
					Name = "Clyde",
					Description = "Contrary to popular belief, Lorem Ipsum is not simply random text.",
					Trips = new List<Trip>{Trips[2], Trips[3]}
				}
			});
		}
	}
}