using mialco.shopping.connector.GoogleAdFeed;
using mialco.shopping.connector.Orchestrator;
using mialco.shopping.connector.shared;
using mialco.shopping.connector.StoreFront;
using mialco.workflow.initiator;
using mialco.workflow.manager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowInitiatorConsole
{
	class Program
	{
		static void Main(string[] args)
		{


			FeedGenerator fg = new FeedGenerator();
			FeedProperties fp = new FeedProperties("Amore T-shirts", "http://www.amoretees.com/", "Amore T-shirts");
			fg.GenerateXmlFeed(@"C:\data\test.xml",null, fp);
			
			Console.WriteLine("Workflow Initiator Console startting");
			CancellationToken _cancelationToken = new CancellationToken(false);
			//ProductReporsitoryEF productRepositoryEF = new ProductReporsitoryEF();
			//var prods = productRepositoryEF.GetAllInStore(7);
			//StoreFrontStoreRepositoryEF storerep = new StoreFrontStoreRepositoryEF();

			//var stores = storerep.GetAll();
			//var store = storerep.GetById(7); 

			var orch = new StoreFrontOrchestratorZero(33,WebStoreDeploymentType.Production);
			orch.Run();

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
	

