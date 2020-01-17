using mialco.abstractions;
using mialco.storage.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.messagehub
{
	public class MialcoInMemoryMessageHub : IMessageHub
	{

		// Implementing a singelton
		private static MialcoInMemoryMessageHub _messageHub ;
		private static object _padlock = new object();
		private Queue<WorkflowMessage> _queue = new Queue<WorkflowMessage>();

		private MialcoInMemoryMessageHub()
		{
		}

		static MialcoInMemoryMessageHub()
		{
			_messageHub = new MialcoInMemoryMessageHub() ;
		}

		public static MialcoInMemoryMessageHub GetInstance()
		{
			lock (_padlock)
			{
				return _messageHub;
			}
		}

		public WorkflowMessage GetMessage()
		{
			if (_queue.Count > 0)
			{
				return _queue.Dequeue();
			}
			else
			{
				return null;
			}
		}


		public void PushMessage(WorkflowMessage message)
		{
			if (message != null)
			{
				lock (_padlock)
				{
					_queue.Enqueue(message);
				}
			}
		}

	}
}
