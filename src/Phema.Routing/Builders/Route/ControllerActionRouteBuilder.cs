namespace Phema.Routing
{
	/// <summary>
	/// Билдер для маршрутов методов. Расширения на нем не применить к контроллерам
	/// </summary>
	public interface IControllerActionRouteBuilder : IRouteBuilder<IControllerActionRouteBuilder>
	{
	}
	
	internal sealed class ControllerActionRouteBuilder
		: RouteBuilder<IControllerActionRouteBuilder>, IControllerActionRouteBuilder
	{
		public ControllerActionRouteBuilder(RouteDeclaration declaration) : base(declaration)
		{
		}
	}
}