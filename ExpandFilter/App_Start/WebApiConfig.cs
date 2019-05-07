using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ExpandFilter.Models;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;

namespace ExpandFilter
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapODataServiceRoute("odata", "api", GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
			config.Expand();
			config.Filter();
			config.Count();
			config.OrderBy();
			config.EnsureInitialized();
		}
		private static IEdmModel GetEdmModel()
		{
			var builder = new ODataConventionModelBuilder
			{
				ContainerName = "DefaultContainer"
			};
			builder.EntitySet<PersonRest>("People");
			builder.EntitySet<TripRest>("Trips");
			var edmModel = builder.GetEdmModel();
			return edmModel;
		}
	}
}
