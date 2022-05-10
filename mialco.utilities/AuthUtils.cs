using mialco.shopping.connector.shared;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace mialco.utilities
{
	public class AuthUtils
	{
		public static AuthChallengeDataResponse GenerateCodeChallenge()
		{

		//Code example here:
		// https://developers.tapkey.io/api/authentication/pkce/

			var rnd  = RandomNumberGenerator.Create();

			var rndBytes = new byte[32];
			rnd.GetBytes(rndBytes);

			// It is recommended to use a URL-safe string as code_verifier.
			// See section 4 of RFC 7636 for more details.
			var verificationCode = Convert.ToBase64String(rndBytes)
				.TrimEnd('=')
				.Replace('+', '-')
				.Replace('/', '_');

			var challengeCode  = string.Empty;
			using (var sha256 = SHA256.Create())
			{
				var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(verificationCode));
				challengeCode = Convert.ToBase64String(challengeBytes);
					//.TrimEnd('=')
					//.Replace('+', '-')
					//.Replace('/', '_');
			}
			var stateCode = Guid.NewGuid().ToString();
			var challengeDataResponse = new AuthChallengeDataResponse(stateCode, challengeCode, StringConstants.ChallengeMethodSHA256, verificationCode);
			return challengeDataResponse;
		}


	}
}
