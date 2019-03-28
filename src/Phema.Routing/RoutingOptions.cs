using System.Reflection;
using System.Collections.Generic;

namespace Phema.Routing
{
	internal sealed class RoutingOptions
	{
		public RoutingOptions()
		{
			Parameters = new Dictionary<ParameterInfo, ParameterDeclaration>();
			Controllers = new Dictionary<TypeInfo, RouteDeclaration>();
			Actions = new Dictionary<MemberInfo, RouteDeclaration>();
		}

		public IDictionary<MemberInfo, RouteDeclaration> Actions { get; }
		public IDictionary<TypeInfo, RouteDeclaration> Controllers { get; }
		public IDictionary<ParameterInfo, ParameterDeclaration> Parameters { get; }
	}
}