using mialco.abstractions;
using mialco.shopping.connector.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.workflow.entities
{
    public class WorkflowDefinition: AggregateRoot
    {

		public IEnumerable<WorkflowStep> WorkflowSteps { get; set; }
		
    }

}
