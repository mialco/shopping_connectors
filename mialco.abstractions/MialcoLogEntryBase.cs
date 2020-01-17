using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
    public class MialcoLogEntryBase
    {
			public string Message { get; set; }
			public string ReferenceId { get; set; }
			public string Caller { get; set; }
			public string TimeStamp { get; set; }
			public string LogType { get; set; }
	}
}
