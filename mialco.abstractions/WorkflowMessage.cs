using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace mialco.abstractions
{
	public abstract class WorkflowMessage
	{
		private Guid _correlationId;
		private long _worklfowDefinitionId;
		private long _WorkflowStepId;
		public WorkflowMessage(long workflowDefinitionId, long workflowStepId)
		{
			_correlationId = new Guid();
			_worklfowDefinitionId=workflowDefinitionId;
			_WorkflowStepId = workflowStepId;
		}


		public Guid CorrelationId { get=>_correlationId;}
		public long WorkflowDefinitionId { get => _worklfowDefinitionId; }
		public long WorkflowStepId { get => _WorkflowStepId; }
		public int Priority { get; set; }
		public IRunable ObjectToRun { get; set; }

	}
}
