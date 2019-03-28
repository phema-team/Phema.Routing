using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Phema.Routing
{
	internal sealed class ApiRequestMetadataProvider : IApiRequestMetadataProvider
	{
		private readonly string[] contentTypes;

		public ApiRequestMetadataProvider(string[] contentTypes)
		{
			this.contentTypes = contentTypes;
		}
		
		public void SetContentTypes(MediaTypeCollection mediaTypes)
		{
			foreach (var contentType in contentTypes)
			{
				mediaTypes.Add(contentType);
			}
		}
	}
}