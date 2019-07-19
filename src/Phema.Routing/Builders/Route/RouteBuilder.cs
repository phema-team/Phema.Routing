namespace Phema.Routing
{
	/// <summary>
	/// Базовый билдер для маршрутов. Методы на нем применяются как к контроллерам, так и к методам
	/// </summary>
	public interface IRouteBuilder<TBuilder>
		where TBuilder : IRouteBuilder<TBuilder>
	{
		IRouteDeclaration Declaration { get; }
	}

	internal abstract class RouteBuilder<TBuilder> : IRouteBuilder<TBuilder>
		where TBuilder : class, IRouteBuilder<TBuilder>
	{
		protected RouteBuilder(RouteDeclaration declaration)
		{
			Declaration = declaration;
		}

		public IRouteDeclaration Declaration { get; }
	}
}