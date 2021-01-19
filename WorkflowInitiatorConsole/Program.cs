using mialco.shopping.connector.GoogleAdFeed;
using mialco.shopping.connector.Orchestrator;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.shared;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFront.GoogleCategoryMapping;
using mialco.workflow.initiator;
using mialco.workflow.manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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


			var orch = new StoreFrontOrchestratorZero(33, WebStoreDeploymentType.Production);
			orch.Run();


				var recordDict = new Dictionary<string, string> {
				{ "Title", "New Bride and Groom Wedding Baseball Caps-Black" },
				{ "Description","You are getting both the Bride and the Groom Cap.Have fun with these.One size fits all.Great wedding gift.Fun to wear prior to the wedding to parties and at the reception for the honeymoon and bachelor party.They are made 100 % cotton, with a beautiful embroidery" },
				{ "ProductType", "Blowout Sale!" },
				{ "Category" , "Apparel & amp; Accessories & gt; Clothing & gt; Shirts & amp; Tops"},
				{ "Link" , "http://www.amoretees.com/p-306-new-bride-and-groom-wedding-baseball-caps-black.aspx" },
				{ "ImageLink" , "http://www.amoretees.com/images/product/medium/306.jpg"},
				{ "AdditionalImageLink" , "http://www.amoretees.com/images/product/medium/306.jpg"},
				{ "Condition" , "new"},
				{ "Availability", "InStock"},
				{ "Price", "34.9900 USD"},
				{ "SalePrice","32.222 USD" }


			};

			var feedRecord = new GenericFeedRecord { Id = 1, ProductId = "1", FeedRecord = recordDict };
			var feedRecords = new List<GenericFeedRecord> { feedRecord };

			GoogleCategoryMapping googleCategoryMapping = new GoogleCategoryMapping();
			googleCategoryMapping.Initialize();
			fg.GenerateXmlFeed(@"C:\data\test.xml",feedRecords, fp);
			
			Console.WriteLine("Workflow Initiator Console startting");
			CancellationToken _cancelationToken = new CancellationToken(false);


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

		static void RunMyTests()
		{
			OrderedDictionary od = new OrderedDictionary {
				{"z" ,"1"},{"t","2" },{"s","3" },{ "a","4"}
			};

			foreach (DictionaryEntry item in od)
			{
				Console.WriteLine($" Key: {item.Key} Val: {item.Value}");
			}


			//var gt = new GenericTreeTest();
			//gt.ShouldGetTheCorrectcategories();

			//ProductReporsitoryEF productRepositoryEF = new ProductReporsitoryEF();
			//var prods = productRepositoryEF.GetAllInStore(7);
			//StoreFrontStoreRepositoryEF storerep = new StoreFrontStoreRepositoryEF();

			//var stores = storerep.GetAll();
			//var store = storerep.GetById(7); 


		}
	}
}
	

