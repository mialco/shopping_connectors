using mialco.abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace mialco.utilities
{
    public class MialcoLogger: IMialcoLogger
    {
		private List<MialcoLogEntryBase> _logs { get; set; }

		public void LogWarning(string message, string referenceId, [CallerMemberName] string caller = "")
		{
			WriteLog(message, referenceId, caller, LogType.Warning);
		}

		public void LogInfo(string message, string referenceId, [CallerMemberName] string caller = "")
		{
			WriteLog(message, referenceId, caller, LogType.Info);
		}

		public void LogError(string message, string referenceId, [CallerMemberName] string caller = "")
		{
			WriteLog(message, referenceId, caller, LogType.Error);
		}

		private void WriteLog(string message, string referenceId, string caller, LogType type)
		{
			MialcoLogEntryBase log = new MialcoLogEntry
			{
				Message = message,
				ReferenceId = referenceId,
				Caller = caller,
				TimeStamp = DateTime.Now.ToString(),
				LogType = type.ToString()
			};

			_logs.Add(log);
		}

	}
}
