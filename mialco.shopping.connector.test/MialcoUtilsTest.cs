using System.Collections.Generic;
using System.Text;
using Xunit;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.GoogleAdFeed;
using System.IO;
using mialco.utilities;

namespace mialco.shopping.connector.test
{
	public class MialcoUtilsTest
	{
		[Fact]
		public void Should_Generate_Auth_Challenge()
		{
			var authData1 = AuthUtils.GenerateCodeChallenge();
			var authData2 = AuthUtils.GenerateCodeChallenge();

			Assert.NotNull(authData2);
			Assert.NotNull(authData2);

		}
	}
}
