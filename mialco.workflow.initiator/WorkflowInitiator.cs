using mialco.messagehub;
using mialco.shopping.repositories;
using mialco.storage.abstracts;
using mialco.utilities;
using mialco.workflow.entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace mialco.workflow.initiator
{
	/// <summary>
	/// Workflow initiator role is to cycle through all the workflow definitions 
	/// and start each worflow by sending a message to the message queue with the respective queue id and stepid=1
	/// </summary>
   public  class WorkflowInitiator
    {
		public CancellationToken _cancelationToken = new CancellationToken(false);
		private static object _padlock = new object();
#pragma warning disable CS0169 // The field 'WorkflowInitiator._activeThreads' is never used
		private static int _activeThreads;
#pragma warning restore CS0169 // The field 'WorkflowInitiator._activeThreads' is never used
		private IMessageHub _messageHub;
		private IRepository<WorkflowDefinition> _workflowDefinitionsRepository;

		public WorkflowInitiator()
		{
			_messageHub = MialcoInMemoryMessageHub.GetInstance();
			_workflowDefinitionsRepository = new WorkflowDefinitionInMemoryRepository();
		}
		public void RunWorkflowIntiator()
		{
			Console.WriteLine($"Starting workflow initiator");

			while (!_cancelationToken.IsCancellationRequested)
			{
				Console.WriteLine($"Looking for Workflows to start");

				try
				{
					//Get the workflows from the database
					var wfdfs =_workflowDefinitionsRepository.GettAll();
					// Cash the workflows

					// For each workflow, 
					// Verify the workflow condition 
					// Send start mesage 

					foreach (var wfd in wfdfs)
					{
						Console.WriteLine($"Invoking StartFirstStep for worflow {wfd.Name}");
						MialcoProcessingMessage mpm = new MialcoProcessingMessage(wfd.Id, 1);
						_messageHub.PushMessage(mpm);
					}
					// Record the 

					//Task t = Task.Factory.StartNew(
					//	() =>
					//	{
					//		IncrementPool();
					//		Console.WriteLine("Started a new task - " + this.ToString());
						   Thread.Sleep(1000);
					//		Console.WriteLine("Ending task - " + this.ToString());
					//		DecrementPool();
					//	}
					//	);
				}
				catch (Exception ex)
				{

					Console.WriteLine("Exception Occured in the Workflow initiator main loop");
					Console.WriteLine(ex.ToString());
				}
				Console.WriteLine("Workflow Initiator is Sleeping ...." + DateTime.Now.ToLongTimeString());
				Thread.Sleep(5000);
			}


		}
	}
}
