using mialco.shopping.connector.StoreFront;
using mialco.workflow.initiator;
using mialco.workflow.manager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Workflow Initiator Console startting");
			CancellationToken _cancelationToken = new CancellationToken(false);

			//ProductReporsitoryEF productRepositoryEF = new ProductReporsitoryEF();
			//var prods = productRepositoryEF.GetAllInStore(7);
			StoreFrontRepositoryEF storerep = new StoreFrontRepositoryEF();
			var stores = storerep.GetAll();
			//var store = storerep.GetById(7); 

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
