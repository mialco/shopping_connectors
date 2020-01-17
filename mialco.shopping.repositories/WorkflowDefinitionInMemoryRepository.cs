using mialco.abstractions;
using mialco.workflow.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.repositories
{
	public class WorkflowDefinitionInMemoryRepository : IRepository<WorkflowDefinition>
	{
		public WorkflowDefinition GetById(long id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<WorkflowDefinition> GettAll()
		{
			var result = new List<WorkflowDefinition>
			{
				new WorkflowDefinition
				{
					WorkflowSteps= new List<WorkflowStep>
					{ },
					Description = "Extract Data from database",
					Name="Extract DB Data",
				}
			};

			return result;
		}
	}
}
