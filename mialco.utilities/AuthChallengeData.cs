using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.utilities
{
	public class AuthChallengeDataResponse
	{
		public AuthChallengeDataResponse(string stateCode, string challengeCode, string challengeMethod, string verificationCode = null)
		{
			StateCode = stateCode;
			ChallengeCode = challengeCode;
			ChallengeCodeMethod = challengeMethod;
			VerificationCode = verificationCode;
		}
		public string StateCode { get; }
		public string ChallengeCode { get; }
		public string ChallengeCodeMethod { get; }
		public string VerificationCode { get; }
	}
}
