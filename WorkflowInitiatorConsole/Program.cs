using mialco.workflow.initiator;
using mialco.workflow.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mialco.shopping.connector.frontstore.repositories;

namespace WorkflowInitiatorConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Workflow Initiator Console startting");
			CancellationToken _cancelationToken = new CancellationToken(false);

			ProductReporsitoryEF productRepositoryEF = new ProductReporsitoryEF();
			var prods = productRepositoryEF.GetAll();

			WorkflowInitiator wi = new WorkflowInitiator();
			//wi.RunWorkflowIniator();
			WorkflowManager wm = new WorkflowManager();
			Task t1 = Task.Factory.StartNew(
				() =>
				{
					wi.RunWorkflowIniator();
				}
			);
			Task t2 = Task.Factory.StartNew(
				() => wm.RunWorkflow()
				);
			t1.Wait(_cancelationToken);
		}
	}
}
