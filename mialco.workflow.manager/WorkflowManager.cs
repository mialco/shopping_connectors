using mialco.abstractions;
using mialco.messagehub;
using mialco.storage.abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mialco.workflow.manager
{
	public class WorkflowManager
	{
		public CancellationToken _cancelationToken = new CancellationToken(false);
		private static object _padlock = new object();
		private static int _activeThreads;
		private IMessageHub _iMessageHub;

		public WorkflowManager()
		{
			_iMessageHub = MialcoInMemoryMessageHub.GetInstance();
		}

		public void RunWorkflow()
		{

			while (!_cancelationToken.IsCancellationRequested)
			{
				Console.WriteLine($"Threads in pool: {ThreadsInThreadPool}");
				//Task t = Task.Factory.StartNew(
				//	() =>
				//	{
				//		IncrementPool();
				//		Console.WriteLine("Started a new task - " + this.ToString());
				//		Thread.Sleep(100);
				//		Console.WriteLine("Ending task - " + this.ToString());
				//		DecrementPool();
				//	}
				//	);
				ThreadPool.QueueUserWorkItem(ThreadpoolTask);

				Console.WriteLine("Sleeping ...." + DateTime.Now.ToLongTimeString());
				Thread.Sleep(5000);
			}


		}

		private static void IncrementPool()
		{
			lock (_padlock)
			{
				_activeThreads++;
			}
		}

		private static void DecrementPool()
		{
			lock (_padlock)
			{
				_activeThreads++;
			}
		}

		public static int ThreadsInThreadPool
		{
			get
				{
					lock (_padlock)
					{
						return _activeThreads;
					}
			}
		}

		
		//public IEnumerable<
		public void ThreadpoolTask(object stateInfo)
		{
			IncrementPool();
			var msg = 	_iMessageHub.GetMessage();
			if (msg != null)
			{
				Console.WriteLine("********** Got a new message from the queue");
				Console.WriteLine("Processing the message retrieved from the queue");
				//ToDO: Execute the OPeration
				//After Operation is executed Go to the workflow definitions
				// and retrieve the next task ans send a message to the queue
				ProcessNextOperation(msg);	
			}
			else
			{
				Console.WriteLine("No message from the queue");
			}

			Console.WriteLine("Executing threadpool task ...." + DateTime.Now.ToLongTimeString());
			Thread.Sleep(4500);
			Console.WriteLine("Finishing threadpool task ...." + DateTime.Now.ToLongTimeString());
			DecrementPool();
		}

		/// <summary>
		/// After finishing running the task, prepare to launch the next operation
		/// </summary>
		/// <param name="msg"></param>
		private void ProcessNextOperation(WorkflowMessage msg)
		{
			var wdfid = msg.WorkflowDefinitionId;
			var wstpid = msg.WorkflowStepId;
			//Get from the repository the stepId 
		}


	}
}