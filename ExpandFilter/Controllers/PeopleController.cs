using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using ExpandFilter.Datasource;
using ExpandFilter.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.UriParser;

namespace ExpandFilter.Controllers
{
	public class PeopleController : ODataController
	{
		private static readonly MapperConfiguration Config = new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<Person, PersonRest>().ForMember(x => x.Trips, m =>
			{
				m.ExplicitExpansion();
			});
			cfg.CreateMap<Trip, TripRest>();
			cfg.AddExpressionMapping();
		});
		private IQueryable<Person> query => DataSource.Instance.People.AsQueryable();
		private IMapper mapper => Config.CreateMapper();
		private static IEnumerable<string> GetExpands(SelectExpandClause option, string expand = "")
		{
			var items = option?.SelectedItems.OfType<ExpandedNavigationSelectItem>().ToList();
			if (items?.Any() == true)
			{
				foreach (var item in items)
				{
					var path = $"{(expand.Length > 0 ? expand + "." : "")}";
					path += string.Join(".", item.PathToNavigationProperty.OfType<NavigationPropertySegment>().Select(s => s.NavigationProperty.Name));
					foreach (var e in GetExpands(item.SelectAndExpand, path))
					{
						yield return e;
					}
				}
			}
			else if (expand.Length > 0)
			{
				yield return expand;
			}
		}
		[EnableQuery]
		public IHttpActionResult Get(ODataQueryOptions<PersonRest> options)
		{
			return Ok(query.ProjectTo<PersonRest>(mapper.ConfigurationProvider, null, GetExpands(options.SelectExpand?.SelectExpandClause).ToArray())); //works
			return Ok(query.UseAsDataSource(mapper).For<PersonRest>(null, GetExpands(options.SelectExpand?.SelectExpandClause).ToArray())); //ArgumentException
		}
	}
}