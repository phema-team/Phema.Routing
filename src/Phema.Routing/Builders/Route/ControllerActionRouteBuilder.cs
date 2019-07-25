namespace Phema.Routing
{
	/// <summary>
	/// Билдер для маршрутов методов. Расширения на нем не применить к контроллерам
	/// </summary>
	public interface IActionRouteBuilder : IRouteBuilder<IActionRouteBuilder>
	{
	}
	
	internal sealed class ActionRouteBuilder
		: RouteBuilder<IActionRouteBuilder>, IActionRouteBuilder
	{
		public ActionRouteBuilder(RouteDeclaration declaration) : base(declaration)
		{
		}
	}
}