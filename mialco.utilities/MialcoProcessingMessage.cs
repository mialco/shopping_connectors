using mialco.abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.utilities
{
    public class MialcoProcessingMessage: WorkflowMessage
    {
		public MialcoProcessingMessage(long workflowId, long workflowStepId): base(workflowId, workflowStepId)
		{
			
		}
    }
}
