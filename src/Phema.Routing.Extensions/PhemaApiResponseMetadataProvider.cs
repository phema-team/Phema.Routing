using System;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Phema.Routing
{
	internal sealed class PhemaApiResponseMetadataProvider : IApiResponseMetadataProvider
	{
		private readonly string[] contentTypes;

		public PhemaApiResponseMetadataProvider(Type type, int statusCode, string[] contentTypes)
		{
			this.contentTypes = contentTypes;
			Type = type;
			StatusCode = statusCode;
		}
		
		public void SetContentTypes(MediaTypeCollection contentTypes)
		{
			foreach (var contentType in this.contentTypes)
			{
				contentTypes.Add(contentType);
			}
		}

		public Type Type { get; }
		public int StatusCode { get; }
	}
}