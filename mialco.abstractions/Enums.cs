namespace mialco.abstractions
{
	public enum WorkflowStatus
	{
		Received = 1,
		Started = 2,
		Failed = 4,
		Finished = 8,
		Error = 16,
		WaitingForApproval = 6
	}

	public enum WorkflowStartMode
	{
		FileWatcher = 1,
		EmailWatcher= 2,
		Scheduller = 4
	}

	public enum MessageType
	{

	}

	public enum LogType
	{
		Info = 0,
		Warning = 1,
		Error = 2
	}

}