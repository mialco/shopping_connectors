using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
	/// <summary>
	/// An interface to manage the workflow processing messages
	/// </summary>
    public interface IQueManager
    {
		WorkflowMessage GetNextToProcess(string channel);
		void PushToProcess(WorkflowMessage workflowMessage, string channel); 
    }
}
