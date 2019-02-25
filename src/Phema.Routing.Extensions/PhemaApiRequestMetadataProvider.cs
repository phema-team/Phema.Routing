using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Phema.Routing
{
	internal sealed class PhemaApiRequestMetadataProvider : IApiRequestMetadataProvider
	{
		private readonly string[] contentTypes;

		public PhemaApiRequestMetadataProvider(string[] contentTypes)
		{
			this.contentTypes = contentTypes;
		}
		
		public void SetContentTypes(MediaTypeCollection contentTypes)
		{
			foreach (var contentType in this.contentTypes)
			{
				contentTypes.Add(contentType);
			}
		}
	}
}