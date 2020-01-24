using mialco.abstractions;
using System.Collections.Generic;

namespace mialco.workflow.entities
{
	public class WorkflowDefinition: AggregateRoot
    {

		public IEnumerable<WorkflowStep> WorkflowSteps { get; set; }
		
    }

}
