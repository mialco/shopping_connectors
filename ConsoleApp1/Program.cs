using mialco.shopping.connector.StoreFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
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

		}
	}
}
