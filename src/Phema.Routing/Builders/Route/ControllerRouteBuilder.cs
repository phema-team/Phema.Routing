namespace Phema.Routing
{
	/// <summary>
	/// Билдер для маршрутов контроллера. Расширения на нем не применить к методам
	/// </summary>
	public interface IControllerRouteBuilder : IRouteBuilder<IControllerRouteBuilder>
	{
	}
	
	internal sealed class ControllerRouteBuilder 
		: RouteBuilder<IControllerRouteBuilder>, IControllerRouteBuilder
	{
		public ControllerRouteBuilder(RouteDeclaration declaration) : base(declaration)
		{
		}
	}
}