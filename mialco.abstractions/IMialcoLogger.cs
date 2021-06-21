using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace mialco.abstractions
{
   public  interface IMialcoLogger
    {
			void LogWarning(string message, string referenceId, [CallerMemberName] string caller = "");
			void LogInfo(string message, string referenceId, [CallerMemberName] string caller = "");
			void LogError(string message, string referenceId, [CallerMemberName] string caller = "");

	}
}
