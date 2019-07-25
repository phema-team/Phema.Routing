using System;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Phema.Routing
{
	internal sealed class ApiResponseMetadataProvider<TModel> : IApiResponseMetadataProvider
	{
		private readonly string[] contentTypes;

		public ApiResponseMetadataProvider(int statusCode, string[] contentTypes)
		{
			this.contentTypes = contentTypes;
			Type = typeof(TModel);
			StatusCode = statusCode;
		}
		
		public void SetContentTypes(MediaTypeCollection mediaTypes)
		{
			foreach (var contentType in contentTypes)
			{
				mediaTypes.Add(contentType);
			}
		}

		public Type Type { get; }
		public int StatusCode { get; }
	}
}