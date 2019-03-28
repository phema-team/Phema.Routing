namespace AspNetCoreExample
{
	public class ExampleController
	{
		public int CalculateLength(string value)
		{
			return value.Length;
		}

		public string GenerateString(char character, int count)
		{
			return new string(character, count);
		}
	}
}