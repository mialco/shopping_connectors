using mialco.workflow.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting workflow program");
			WorkflowManager workflowManager = new WorkflowManager();
			workflowManager.RunWorkflow();
			Console.WriteLine("End of workflow program");
			Console.ReadLine();
		}
	}
}
