using Microsoft.AspNetCore.Mvc;

namespace Phema.Routing.Sandbox
{
	public class Controller : ControllerBase
	{
		public string Get(string test)
		{
			return $"{test} get";
		}

		public string Post(int test)
		{
			return $"{test} post";
		}
	}
}