namespace Phema.Routing.MvcExample
{
	public class ExampleController
	{
		public int CalculateLength(string value)
		{
			return value?.Length ?? 0;
		}

		public string GenerateString(char character, int count)
		{
			return new string(character, count);
		}

		public int NamedParameter(int number)
		{
			return 2 * number;
		}

		public string NamedRoute(string message)
		{
			return $"Hello, {message}";
		}
	}
}