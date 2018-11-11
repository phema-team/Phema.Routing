using System.Reflection;
using System.Collections.Generic;

namespace Phema.Routing
{
	internal class RoutingOptions
	{
		public RoutingOptions()
		{
			Parameters = new Dictionary<ParameterInfo, ParameterMetadata>();
			Controllers = new Dictionary<TypeInfo, RouteMetadata>();
			Actions = new Dictionary<MemberInfo, RouteMetadata>();
		}

		public IDictionary<MemberInfo, RouteMetadata> Actions { get; }
		public IDictionary<TypeInfo, RouteMetadata> Controllers { get; }
		public IDictionary<ParameterInfo, ParameterMetadata> Parameters { get; }
	}
}